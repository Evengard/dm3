using System;
using System.Threading.Tasks;
using DM.Services.Authentication.Dto;
using DM.Services.Authentication.Factories;
using DM.Services.Authentication.Implementation;
using DM.Services.Authentication.Implementation.Security;
using DM.Services.Authentication.Repositories;
using DM.Services.Core.Dto.Enums;
using DM.Services.DataAccess.BusinessObjects.Users;
using DM.Tests.Core;
using FluentAssertions;
using Moq;
using Moq.Language.Flow;
using Xunit;

namespace DM.Services.Authentication.Tests
{
    public class AuthenticationServiceLoginShould : UnitTestBase
    {
        private readonly ISetup<IAuthenticationRepository, Task<(bool Success, AuthenticatedUser User)>> userSearchSetup;
        private readonly AuthenticationService service;
        private readonly Mock<IAuthenticationRepository> authenticationRepository;
        private readonly Mock<ISecurityManager> securityManager;
        private readonly Mock<ISessionFactory> sessionFactory;
        private readonly Mock<ISymmetricCryptoService> cryptoService;
        private const string Username = nameof(Username);

        public AuthenticationServiceLoginShould()
        {
            securityManager = Mock<ISecurityManager>();
            cryptoService = Mock<ISymmetricCryptoService>();
            authenticationRepository = Mock<IAuthenticationRepository>();
            userSearchSetup = authenticationRepository.Setup(r => r.TryFindUser(It.IsAny<string>()));
            sessionFactory = Mock<ISessionFactory>();
            service = new AuthenticationService(securityManager.Object, cryptoService.Object,
                authenticationRepository.Object, sessionFactory.Object, null, null);
        }

        public override void Dispose()
        {
            authenticationRepository.Verify(r => r.TryFindUser(Username), Times.Once);
            authenticationRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task FailIfNoUserFoundByLogin()
        {
            userSearchSetup.ReturnsAsync((false, null));
            var actual = await service.Authenticate(Username, "qwerty", false);

            actual.Error.Should().Be(AuthenticationError.WrongLogin);
        }

        [Fact]
        public async Task FailIfInactiveUserFound()
        {
            var user = new AuthenticatedUser{Activated = false};
            userSearchSetup.ReturnsAsync((true, user));
            var actual = await service.Authenticate(Username, "qwerty", false);

            actual.Error.Should().Be(AuthenticationError.Inactive);
            actual.User.Should().Be(AuthenticatedUser.Guest);
            actual.Session.Should().BeNull();
            actual.Settings.Should().Be(UserSettings.Default);
            actual.Token.Should().BeNull();
        }

        [Fact]
        public async Task FailIfRemovedUserFound()
        {
            var user = new AuthenticatedUser{Activated = true, IsRemoved = true};
            userSearchSetup.ReturnsAsync((true, user));
            var actual = await service.Authenticate(Username, "qwerty", false);

            actual.Error.Should().Be(AuthenticationError.Removed);
            actual.User.Should().Be(AuthenticatedUser.Guest);
            actual.Session.Should().BeNull();
            actual.Settings.Should().Be(UserSettings.Default);
            actual.Token.Should().BeNull();
        }

        [Fact]
        public async Task FailIfBannedUserFound()
        {
            var user = new AuthenticatedUser
            {
                Activated = true,
                IsRemoved = false,
                AccessPolicy = AccessPolicy.FullBan | AccessPolicy.RestrictContentEditing
            };
            userSearchSetup.ReturnsAsync((true, user));
            var actual = await service.Authenticate(Username, "qwerty", false);

            actual.Error.Should().Be(AuthenticationError.Banned);
            actual.User.Should().Be(AuthenticatedUser.Guest);
            actual.Session.Should().BeNull();
            actual.Settings.Should().Be(UserSettings.Default);
            actual.Token.Should().BeNull();
        }

        [Fact]
        public async Task FailIfPasswordIsIncorrect()
        {
            var user = new AuthenticatedUser
            {
                Activated = true,
                IsRemoved = false,
                AccessPolicy = AccessPolicy.ChatBan | AccessPolicy.DemocraticBan,
                PasswordHash = "hash",
                Salt = "salt"
            };
            userSearchSetup.ReturnsAsync((true, user));
            securityManager
                .Setup(m => m.ComparePasswords(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);
            
            var actual = await service.Authenticate(Username, "qwerty", false);

            actual.Error.Should().Be(AuthenticationError.WrongPassword);
            actual.User.Should().Be(AuthenticatedUser.Guest);
            actual.Session.Should().BeNull();
            actual.Settings.Should().Be(UserSettings.Default);
            actual.Token.Should().BeNull();
            securityManager.Verify(m => m.ComparePasswords("qwerty", "salt", "hash"));
        }

        [Fact]
        public async Task SucceedAndCreateNewUserSession()
        {
            var userId = Guid.NewGuid();
            var sessionId = Guid.NewGuid();
            var session = new Session {Id = sessionId};
            var userSettings = new UserSettings();
            var user = new AuthenticatedUser
            {
                UserId = userId,
                Activated = true,
                IsRemoved = false,
                AccessPolicy = AccessPolicy.NotSpecified,
                PasswordHash = "hash",
                Salt = "salt"
            };
            userSearchSetup.ReturnsAsync((true, user));
            securityManager
                .Setup(m => m.ComparePasswords(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            sessionFactory
                .Setup(f => f.Create(It.IsAny<bool>()))
                .Returns(session);
            authenticationRepository
                .Setup(r => r.FindUserSettings(It.IsAny<Guid>()))
                .ReturnsAsync(userSettings);
            authenticationRepository
                .Setup(r => r.AddSession(It.IsAny<Guid>(), It.IsAny<Session>()))
                .Returns(Task.CompletedTask);
            cryptoService
                .Setup(s => s.Encrypt(It.IsAny<string>()))
                .ReturnsAsync("token");

            var actual = await service.Authenticate(Username, "qwerty", true);

            actual.Error.Should().Be(AuthenticationError.NoError);
            actual.User.Should().Be(user);
            actual.Session.Should().Be(session);
            actual.Settings.Should().Be(userSettings);
            actual.Token.Should().Be("token");
            securityManager.Verify(m => m.ComparePasswords("qwerty", "salt", "hash"));
            sessionFactory.Verify(f => f.Create(true));
            authenticationRepository.Verify(r => r.FindUserSettings(userId), Times.Once);
            authenticationRepository.Verify(r => r.AddSession(userId, session), Times.Once);
        }
    }
}