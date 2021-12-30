using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class AttributeOptions
    {
        public AttributeOptions()
        {
            AttributeOptionValues = new HashSet<AttributeOptionValues>();
        }

        [Key]
        public int Id { get; set; }
        public int? AttributeId { get; set; }
        public int? SortOrder { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty(nameof(Attributes.AttributeOptions))]
        public virtual Attributes Attribute { get; set; }
        [InverseProperty("Option")]
        public virtual ICollection<AttributeOptionValues> AttributeOptionValues { get; set; }
    }
}
