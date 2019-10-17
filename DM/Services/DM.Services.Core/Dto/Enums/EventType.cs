using DM.Services.Core.Extensions;

namespace DM.Services.Core.Dto.Enums
{
    /// <summary>
    /// Message queue event type
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Unknown event
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// New user has been created
        /// </summary>
        [EventRoutingKey("community.user.registered")]
        NewUser = 1,

        /// <summary>
        /// User has been activated
        /// </summary>
        [EventRoutingKey("community.user.activated")]
        ActivatedUser = 2,

        /// <summary>
        /// New topic has been created
        /// </summary>
        [EventRoutingKey("forum.topic.created")]
        NewTopic = 101,

        /// <summary>
        /// Topic text has been updated
        /// </summary>
        [EventRoutingKey("forum.topic.changed")]
        ChangedTopic = 102,

        /// <summary>
        /// Topic has been deleted
        /// </summary>
        [EventRoutingKey("forum.topic.deleted")]
        DeletedTopic = 104,

        /// <summary>
        /// New commentary has been created on forum
        /// </summary>
        [EventRoutingKey("forum.comment.created")]
        NewForumComment = 201,

        /// <summary>
        /// Forum commentary has been updated
        /// </summary>
        [EventRoutingKey("forum.comment.changed")]
        ChangedForumComment = 202,

        /// <summary>
        /// Forum commentary has been deleted
        /// </summary>
        [EventRoutingKey("forum.comment.deleted")]
        DeletedForumComment = 203,

        /// <summary>
        /// Topic has been liked
        /// </summary>
        [EventRoutingKey("forum.topic.liked")]
        LikedTopic = 601,

        /// <summary>
        /// Forum commentary has been liked
        /// </summary>
        [EventRoutingKey("forum.comment.liked")]
        LikedForumComment = 602,

        /// <summary>
        /// New game has been created
        /// </summary>
        [EventRoutingKey("game.module.created")]
        NewGame = 301,

        /// <summary>
        /// Game has been updated
        /// </summary>
        [EventRoutingKey("game.module.changed")]
        ChangedGame = 302,

        /// <summary>
        /// Game has been deleted
        /// </summary>
        [EventRoutingKey("game.module.deleted")]
        DeletedGame = 303,

        /// <summary>
        /// Assistant assignment request created and pending
        /// </summary>
        [EventRoutingKey("game.module.assignment.created")]
        AssignmentRequestCreated = 351,

        /// <summary>
        /// Assistant assignment request has been accepted
        /// </summary>
        [EventRoutingKey("game.module.assignment.accepted")]
        AssignmentRequestAccepted = 352,

        /// <summary>
        /// Assistant assignment request has been rejected
        /// </summary>
        [EventRoutingKey("game.module.assignment.rejected")]
        AssignmentRequestRejected = 353
    }
}