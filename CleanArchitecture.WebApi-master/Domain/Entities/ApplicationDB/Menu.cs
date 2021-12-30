using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Menu
    {
        [Key]
        [Column("nameCode")]
        [StringLength(255)]
        public string nameCode { get; set; }
        [Column("nameMenu")]
        [StringLength(255)]
        public string nameMenu { get; set; }
    }
}
