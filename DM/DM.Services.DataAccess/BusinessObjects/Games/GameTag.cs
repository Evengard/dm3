using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DM.Services.DataAccess.BusinessObjects.Common;

namespace DM.Services.DataAccess.BusinessObjects.Games
{
    [Table("GameTags")]
    public class GameTag
    {
        [Key]
        public Guid GameTagId { get; set; }

        public Guid GameId { get; set; }
        public Guid TagId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }

        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
    }
}