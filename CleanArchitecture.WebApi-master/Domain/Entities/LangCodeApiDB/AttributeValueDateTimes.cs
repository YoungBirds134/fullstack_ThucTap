using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class AttributeValueDateTimes
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "ntext")]
        public string Value { get; set; }
        public int? AttributeId { get; set; }
        public int? ProductId { get; set; }
        [StringLength(50)]
        public string LanguageId { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty(nameof(Attributes.AttributeValueDateTimes))]
        public virtual Attributes Attribute { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Products.AttributeValueDateTimes))]
        public virtual Products Product { get; set; }
    }
}
