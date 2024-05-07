using N_Tier.Core.Common;
using N_Tier.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace N_Tier.Core.Entities
{
    [Table("loans")]
    public class Loan : BaseEntity
    {
        [Column("loan_date")]
        public DateTime LoanDate { get; set; }
        [Column("due_date")]
        public DateTime DueDate { get; set; }
        [Column("return_date")]
        public DateTime? ReturnDate { get; set; }
        [Column("fine")]
        public decimal Fine { get; set; }
        [ForeignKey("customer_id")]
        public ApplicationUser Customer { get; set; }
        [ForeignKey("librarian_id")]
        public ApplicationUser Librarian { get; set; }
        [ForeignKey("book_id")]
        public Book Book { get; set; }
    }
}
