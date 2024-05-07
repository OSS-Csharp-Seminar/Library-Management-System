using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace N_Tier.Core.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ApplicationRole
    {
        [Display(Name = "Administrator")]
        Administrator,
        [Display(Name = "Librarian")]
        Librarian,
        [Display(Name = "Customer")]
        Customer
    }
}
