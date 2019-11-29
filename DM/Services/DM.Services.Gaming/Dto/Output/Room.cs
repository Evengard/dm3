using System;
using DM.Services.Core.Dto.Enums;

namespace DM.Services.Gaming.Dto.Output
{
    /// <summary>
    /// DTO Model for game room
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Room title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Room content type
        /// </summary>
        public RoomType Type { get; set; }

        /// <summary>
        /// Room access type
        /// </summary>
        public RoomAccessType AccessType { get; set; }

        /// <summary>
        /// Unread posts count
        /// </summary>
        public int UnreadPostsCount { get; set; }

        /// <summary>
        /// Room order number
        /// </summary>
        public double OrderNumber { get; set; }

        /// <summary>
        /// Previous room identifier
        /// </summary>
        public Guid? PreviousRoomId { get; set; }
    }
}