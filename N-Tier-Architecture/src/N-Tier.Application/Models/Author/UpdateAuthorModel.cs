using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Models.Author
{
    public class UpdateAuthorModel
    {
        [Required]
        [Display(Name = "Firstname")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "DateOfBirth")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [Display(Name = "About")]
        [DataType(DataType.Text)]
        public string About { get; set; }
    }

    public class UpdateAuthorReponseModel : BaseResponseModel { }
}
