using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DM.Services.Authentication.Dto;
using DM.Services.Authentication.Implementation.UserIdentity;
using DM.Services.Common.Authorization;
using DM.Services.Common.Dto;
using DM.Services.Core.Dto;
using DM.Services.Core.Exceptions;
using DM.Services.Gaming.Authorization;
using DM.Services.Gaming.BusinessProcesses.Games.Reading;

namespace DM.Services.Gaming.BusinessProcesses.Commentaries.Reading
{
    /// <inheritdoc />
    public class CommentaryReadingService : ICommentaryReadingService
    {
        private readonly IIntentionManager intentionManager;
        private readonly IGameReadingService gameReadingService;
        private readonly ICommentaryReadingRepository commentaryRepository;
        private readonly IIdentity identity;

        /// <inheritdoc />
        public CommentaryReadingService(
            IIntentionManager intentionManager,
            IGameReadingService gameReadingService,
            ICommentaryReadingRepository commentaryRepository,
            IIdentityProvider identityProvider)
        {
            this.intentionManager = intentionManager;
            this.gameReadingService = gameReadingService;
            this.commentaryRepository = commentaryRepository;
            identity = identityProvider.Current;
        }
        
        /// <inheritdoc />
        public async Task<(IEnumerable<Comment> comments, PagingResult paging)> Get(Guid gameId, PagingQuery query)
        {
            var game = await gameReadingService.GetGame(gameId);
            await intentionManager.ThrowIfForbidden(GameIntention.ReadComments, game);

            var totalCount = await commentaryRepository.Count(gameId);
            var paging = new PagingData(query, identity.Settings.CommentsPerPage, totalCount);

            var comments = await commentaryRepository.Get(gameId, paging);

            return (comments, paging.Result);
        }

        /// <inheritdoc />
        public async Task<Comment> Get(Guid commentId)
        {
            return await commentaryRepository.Get(commentId) ??
                throw new HttpException(HttpStatusCode.Gone, $"Comment {commentId} not found");
        }
    }
}