using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class AttributeOptionValues
    {
        [Key]
        public int Id { get; set; }
        public int? OptionId { get; set; }
        [StringLength(255)]
        public string Value { get; set; }

        [ForeignKey(nameof(OptionId))]
        [InverseProperty(nameof(AttributeOptions.AttributeOptionValues))]
        public virtual AttributeOptions Option { get; set; }
    }
}
