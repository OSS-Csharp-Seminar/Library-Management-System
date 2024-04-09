using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Column("release_date")]
        public DateTime ReleaseDate { get; set; }
        [Column("availability")]
        public Boolean Availability { get; set; }
        [Column("status")]
        public Status Status { get; set; }
        [ForeignKey("work_id")]
        public Work Work { get; set; }
    }
}
