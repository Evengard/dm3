using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DM.Services.Core.Dto;
using DM.Services.Core.Extensions;
using DM.Services.DataAccess;
using DM.Services.DataAccess.BusinessObjects.Fora;
using DM.Services.DataAccess.RelationalStorage;
using DM.Services.Forum.Dto.Output;
using Microsoft.EntityFrameworkCore;

namespace DM.Services.Forum.BusinessProcesses.Commentaries

{
    /// <inheritdoc />
    public class CommentRepository : ICommentRepository
    {
        private readonly DmDbContext dbContext;

        private readonly IMapper mapper;

        /// <inheritdoc />
        public CommentRepository(
            DmDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public Task<int> Count(Guid topicId) => dbContext.Comments
            .CountAsync(c => !c.IsRemoved && c.ForumTopicId == topicId);

        /// <inheritdoc />
        public async Task<IEnumerable<Comment>> Get(Guid topicId, PagingData paging)
        {
            return await dbContext.Comments
                .Where(c => !c.IsRemoved && c.ForumTopicId == topicId)
                .OrderBy(c => c.CreateDate)
                .Page(paging)
                .ProjectTo<Comment>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }


        /// <inheritdoc />
        public Task<Comment> Get(Guid commentId)
        {
            return dbContext.Comments
                .Where(c => !c.IsRemoved && c.ForumCommentId == commentId)
                .ProjectTo<Comment>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<Comment> Create(ForumComment comment, UpdateBuilder<ForumTopic> topicUpdate)
        {
            dbContext.Comments.Add(comment);
            topicUpdate.Update(dbContext);
            await dbContext.SaveChangesAsync();
            return await dbContext.Comments
                .Where(c => c.ForumCommentId == comment.ForumCommentId)
                .ProjectTo<Comment>(mapper.ConfigurationProvider)
                .FirstAsync();
        }

        /// <inheritdoc />
        public async Task<Comment> Update(UpdateBuilder<ForumComment> update)
        {
            var commentId = update.Update(dbContext);
            await dbContext.SaveChangesAsync();
            return await dbContext.Comments
                .Where(c => c.ForumCommentId == commentId)
                .ProjectTo<Comment>(mapper.ConfigurationProvider)
                .FirstAsync();
        }
    }
}