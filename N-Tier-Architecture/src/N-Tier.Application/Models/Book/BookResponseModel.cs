using N_Tier.Core.Entities;
using N_Tier.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Models.Book
{
    public class BookResponseModel : BaseResponseModel
    {
        [Required]
        [Display(Name = "ReleaseDate")]
        [DataType(DataType.Date)]
        public DateOnly ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Availability")]
        public Boolean Availability { get; set; }

        [Required]
        [Display(Name = "Status")]
        public Status Status { get; set; }

        [Required]
        [Display(Name = "Work")]
        public N_Tier.Core.Entities.Work Work { get; set; }
    }
}
