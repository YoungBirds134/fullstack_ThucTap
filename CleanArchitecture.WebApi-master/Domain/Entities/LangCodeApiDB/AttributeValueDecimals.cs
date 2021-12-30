using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class AttributeValueDecimals
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string Value { get; set; }
        public int? AttributeId { get; set; }
        public int? ProductId { get; set; }
        [StringLength(50)]
        public string LanguageId { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty(nameof(Attributes.AttributeValueDecimals))]
        public virtual Attributes Attribute { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Products.AttributeValueDecimals))]
        public virtual Products Product { get; set; }
    }
}
