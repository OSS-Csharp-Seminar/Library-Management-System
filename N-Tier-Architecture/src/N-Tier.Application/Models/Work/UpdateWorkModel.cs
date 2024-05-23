using N_Tier.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Models.Work
{
    public class UpdateWorkModel
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
        public Guid AuthorId { get; set; }
    }

    public class UpdateWorkResponseModel : BaseResponseModel { }
}
