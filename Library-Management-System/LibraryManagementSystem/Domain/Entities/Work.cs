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
    [Table("works")]
    public class Work : BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("genre")]
        public Genre Genre { get; set; }
        [ForeignKey("author_id")]
        public Author Author { get; set; }
    }
}
