using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum Status
    {
        [Display(Name = "Available")]
        Available,
        [Display(Name = "InUse")]
        InUse,
        [Display(Name = "Disabled")]
        Disabled
    }
}
