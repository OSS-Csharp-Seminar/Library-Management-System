using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
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
        public User Customer { get; set; }
        [ForeignKey("librarian_id")]
        public User Librarian { get; set; }
        [ForeignKey("book_id")]
        public Book Book { get; set; }
    }
}
