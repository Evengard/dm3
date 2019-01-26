using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.Services.DataAccess.BusinessObjects.Games
{
    [Table("TagGroups")]
    public class TagGroup
    {
        [Key]
        public Guid TagGroupId { get; set; }

        public string Title { get; set; }

        [InverseProperty(nameof(Tag.TagGroup))]
        public ICollection<Tag> Tags { get; set; }
    }
}