using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Language
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("VNLang")]
        [StringLength(255)]
        public string Vnlang { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
        [Column("nameMenu")]
        [StringLength(255)]
        public string NameMenu { get; set; }
        [StringLength(255)]
        public string KoreaLang { get; set; }
        [StringLength(255)]
        public string EngLang { get; set; }
    }
}
