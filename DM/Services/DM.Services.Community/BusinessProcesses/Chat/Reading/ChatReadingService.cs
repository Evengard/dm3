using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DM.Services.Authentication.Implementation.UserIdentity;
using DM.Services.Core.Dto;
using DM.Services.Core.Exceptions;

namespace DM.Services.Community.BusinessProcesses.Chat.Reading
{
    /// <inheritdoc />
    public class ChatReadingService : IChatReadingService
    {
        private readonly IChatReadingRepository repository;
        private readonly IIdentityProvider identityProvider;

        /// <inheritdoc />
        public ChatReadingService(
            IChatReadingRepository repository,
            IIdentityProvider identityProvider)
        {
            this.repository = repository;
            this.identityProvider = identityProvider;
        }

        /// <inheritdoc />
        public async Task<(IEnumerable<ChatMessage> messages, PagingResult paging)> GetMessages(PagingQuery pagingQuery)
        {
            var totalCount = await repository.Count();
            var pageSize = identityProvider.Current.Settings.Paging.MessagesPerPage;
            var pagingData = new PagingData(pagingQuery, pageSize, totalCount);
            var chatMessages = await repository.Get(pagingData);

            return (chatMessages, pagingData.Result);
        }

        /// <inheritdoc />
        public async Task<ChatMessage> GetMessage(Guid id)
        {
            var chatMessage = await repository.Get(id);
            if (chatMessage == null)
            {
                throw new HttpException(HttpStatusCode.Gone, "Message not found");
            }

            return chatMessage;
        }
    }
}