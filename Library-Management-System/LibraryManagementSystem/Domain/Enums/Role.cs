using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
     public enum Role
    {
        [Display(Name = "Administrator")]
        Administrator,
        [Display(Name = "Librarian")]
        Librarian,
        [Display(Name = "Customer")]
        Customer,
    }
}
