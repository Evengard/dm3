using System.Threading.Tasks;
using AutoMapper;
using DM.Services.Community.BusinessProcesses.Registration;
using DM.Services.Community.Dto;
using DM.Web.API.Dto.Users;

namespace DM.Web.API.Services.Users
{
    /// <inheritdoc />
    public class RegistrationApiService : IRegistrationApiService
    {
        private readonly IRegistrationService registrationService;
        private readonly IMapper mapper;

        /// <inheritdoc />
        public RegistrationApiService(
            IRegistrationService registrationService,
            IMapper mapper)
        {
            this.registrationService = registrationService;
            this.mapper = mapper;
        }
        
        /// <inheritdoc />
        public Task Register(Registration registration) =>
            registrationService.Register(mapper.Map<UserRegistration>(registration));
    }
}