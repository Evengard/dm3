using System;
using System.Threading.Tasks;
using DM.Web.API.Dto.Contracts;
using DM.Web.API.Dto.Fora;
using DM.Web.API.Dto.Users;
using DM.Web.API.Services.Fora;
using DM.Web.Core.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DM.Web.API.Controllers.v1.Fora
{
    /// <summary>
    /// Topics
    /// </summary>
    [Route("v1/topics")]
    public class TopicController : Controller
    {
        private readonly ITopicApiService topicApiService;
        private readonly ILikeApiService likeApiService;
        private readonly ICommentApiService commentApiService;

        /// <inheritdoc />
        public TopicController(
            ITopicApiService topicApiService,
            ILikeApiService likeApiService,
            ICommentApiService commentApiService)
        {
            this.topicApiService = topicApiService;
            this.likeApiService = likeApiService;
            this.commentApiService = commentApiService;
        }

        /// <summary>
        /// Get certain topic
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        /// <response code="404">No topic was found for passed id</response>
        [HttpGet("{id}", Name = nameof(GetTopic))]
        [ProducesResponseType(typeof(Envelope<Topic>), 200)]
        [ProducesResponseType(typeof(GeneralError), 404)]
        public async Task<IActionResult> GetTopic(Guid id) => Ok(await topicApiService.Get(id));

        /// <summary>
        /// Put topic changes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="topic">Topic</param>
        /// <response code="200"></response>
        /// <response code="400">Some of topic changed properties were invalid or passed id was not recognized</response>
        /// <response code="401">User must be authenticated</response>
        /// <response code="403">User is not authorized to change some properties of this topic</response>
        /// <response code="404">No topic was found for passed id</response>
        [HttpPut("{id}", Name = nameof(PutTopic))]
        [AuthenticationRequired]
        [ProducesResponseType(typeof(Envelope<Topic>), 200)]
        [ProducesResponseType(typeof(BadRequestError), 400)]
        [ProducesResponseType(typeof(GeneralError), 401)]
        [ProducesResponseType(typeof(GeneralError), 403)]
        [ProducesResponseType(typeof(GeneralError), 404)]
        public async Task<IActionResult> PutTopic(Guid id, [FromBody] Topic topic) =>
            Ok(await topicApiService.Update(id, topic));

        /// <summary>
        /// Delete certain topic
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        /// <response code="401">User must be authenticated</response>
        /// <response code="403">User is not allowed to remove the topic</response>
        /// <response code="404">No topic was found for passed id</response>
        [HttpDelete("{id}", Name = nameof(DeleteTopic))]
        [AuthenticationRequired]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(GeneralError), 401)]
        [ProducesResponseType(typeof(GeneralError), 403)]
        [ProducesResponseType(typeof(GeneralError), 404)]
        public async Task<IActionResult> DeleteTopic(Guid id)
        {
            await topicApiService.Delete(id);
            return NoContent();
        }


        /// <summary>
        /// Post new like
        /// </summary>
        /// <param name="id"></param>
        /// <response code="201"></response>
        /// <response code="401">User must be authenticated</response>
        /// <response code="403">User is not allowed to like the topic</response>
        /// <response code="404">No topic was found for passed id</response>
        /// <response code="409">User already liked this topic</response>
        [HttpPost("{id}/likes", Name = nameof(PostTopicLike))]
        [AuthenticationRequired]
        [ProducesResponseType(typeof(Envelope<User>), 201)]
        [ProducesResponseType(typeof(GeneralError), 401)]
        [ProducesResponseType(typeof(GeneralError), 403)]
        [ProducesResponseType(typeof(GeneralError), 404)]
        [ProducesResponseType(typeof(GeneralError), 409)]
        public async Task<IActionResult> PostTopicLike(Guid id) =>
            CreatedAtRoute(nameof(GetTopic), new {id}, await likeApiService.LikeTopic(id));

        /// <summary>
        /// Delete like
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"></response>
        /// <response code="401">User must be authenticated</response>
        /// <response code="403">User is not allowed to remove like from this topic</response>
        /// <response code="404">No topic was found for passed id</response>
        /// <response code="409">User has no like for this topic</response>
        [HttpDelete("{id}/likes", Name = nameof(DeleteTopicLike))]
        [AuthenticationRequired]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(GeneralError), 401)]
        [ProducesResponseType(typeof(GeneralError), 403)]
        [ProducesResponseType(typeof(GeneralError), 404)]
        [ProducesResponseType(typeof(GeneralError), 409)]
        public async Task<IActionResult> DeleteTopicLike(Guid id)
        {
            await likeApiService.DislikeTopic(id);
            return NoContent();
        }

        /// <summary>
        /// Get list of comments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="n">Entity number</param>
        /// <response code="200"></response>
        /// <response code="404">No topic was found for passed id</response>
        [HttpGet("{id}/comments", Name = nameof(GetForumComments))]
        [ProducesResponseType(typeof(ListEnvelope<Topic>), 200)]
        [ProducesResponseType(typeof(GeneralError), 404)]
        public async Task<IActionResult> GetForumComments(Guid id, [FromQuery] int n = 1) =>
            Ok(await commentApiService.Get(id, n));

        /// <summary>
        /// Post new comment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment">Comment</param>
        /// <response code="201"></response>
        /// <response code="400">Some of comment properties were invalid</response>
        /// <response code="401">User must be authenticated</response>
        /// <response code="403">User is not allowed to comment this topic</response>
        /// <response code="404">No topic was found for passed id</response>
        [HttpPost("{id}/comments", Name = nameof(PostForumComment))]
        [AuthenticationRequired]
        [ProducesResponseType(typeof(Envelope<Comment>), 201)]
        [ProducesResponseType(typeof(BadRequestError), 400)]
        [ProducesResponseType(typeof(GeneralError), 401)]
        [ProducesResponseType(typeof(GeneralError), 403)]
        [ProducesResponseType(typeof(GeneralError), 404)]
        public async Task<IActionResult> PostForumComment(Guid id, [FromBody] Comment comment)
        {
            var result = await commentApiService.Create(id, comment);
            return CreatedAtRoute(nameof(CommentController.GetForumComment), new {id = result.Resource.Id}, result);
        }
    }
}