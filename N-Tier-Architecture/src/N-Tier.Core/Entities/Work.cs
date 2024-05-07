using N_Tier.Core.Common;
using N_Tier.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace N_Tier.Core.Entities
{
    [Table("works")]
    public class Work : BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("genre")]
        public Genre Genre { get; set; }
        [ForeignKey("author_id")]
        public Author Author { get; set; }
    }
}
