using N_Tier.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace N_Tier.Core.Entities
{
    [Table("authors")]
    public class Author : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("date_of_birth")]
        public DateOnly DateOfBirth { get; set; }
        [Column("about")]
        public string About { get; set; }
    }
}
