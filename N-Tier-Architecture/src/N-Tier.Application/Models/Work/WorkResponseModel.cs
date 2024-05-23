using N_Tier.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N_Tier.Core.Entities;

namespace N_Tier.Application.Models.Work
{
    public class WorkResponseModel : BaseResponseModel
    {
        [Required]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Author")]
        public N_Tier.Core.Entities.Author Author { get; set; }
    }
}
