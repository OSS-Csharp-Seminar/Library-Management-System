using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Models.Loan
{
    public class LoanResponseModel : BaseResponseModel
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
        public ApplicationUser Customer { get; set; }

        [Required]
        [Display(Name = "Librarian")]
        public ApplicationUser Librarian { get; set; }

        [Required]
        [Display(Name = "Book")]
        public N_Tier.Core.Entities.Book Book { get; set; }
    }
}
