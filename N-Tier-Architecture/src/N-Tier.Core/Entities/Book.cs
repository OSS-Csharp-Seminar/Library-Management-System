using N_Tier.Core.Common;
using N_Tier.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace N_Tier.Core.Entities
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Column("release_date")]
        public DateOnly ReleaseDate { get; set; }
        [Column("availability")]
        public Boolean Availability { get; set; }
        [Column("status")]
        public Status Status { get; set; }
        [ForeignKey("work_id")]
        public Work Work { get; set; }
    }
}
