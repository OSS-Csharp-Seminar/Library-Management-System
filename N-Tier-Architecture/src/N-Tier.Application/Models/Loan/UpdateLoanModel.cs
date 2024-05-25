using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Models.Loan
{
    public class UpdateLoanModel
    {
        [Required]
        [Display(Name = "LoanDate")]
        [DataType(DataType.DateTime)]
        public DateTime LoanDate { get; set; }

        [Required]
        [Display(Name = "DueDate")]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }


        [Display(Name = "ReturnDate")]
        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }


        [Display(Name = "Fine")]
        [DataType(DataType.Text)]
        public decimal Fine { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public string CustomerId { get; set; }

        [Required]
        [Display(Name = "Librarian")]
        public string LibrarianId { get; set; }

        [Required]
        [Display(Name = "Book")]
        public string Book { get; set; }
    }

    public class UpdateLoanResponseModel : BaseResponseModel { }
}
