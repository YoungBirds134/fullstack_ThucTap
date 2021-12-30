using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Attributes
    {
        public Attributes()
        {
            AttributeOptions = new HashSet<AttributeOptions>();
            AttributeValueDateTimes = new HashSet<AttributeValueDateTimes>();
            AttributeValueDecimals = new HashSet<AttributeValueDecimals>();
            AttributeValueInts = new HashSet<AttributeValueInts>();
            AttributeValueText = new HashSet<AttributeValueText>();
            AttributeValueVarchars = new HashSet<AttributeValueVarchars>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Code { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public int? SortOrder { get; set; }
        [StringLength(50)]
        public string BackendType { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("Attribute")]
        public virtual ICollection<AttributeOptions> AttributeOptions { get; set; }
        [InverseProperty("Attribute")]
        public virtual ICollection<AttributeValueDateTimes> AttributeValueDateTimes { get; set; }
        [InverseProperty("Attribute")]
        public virtual ICollection<AttributeValueDecimals> AttributeValueDecimals { get; set; }
        [InverseProperty("Attribute")]
        public virtual ICollection<AttributeValueInts> AttributeValueInts { get; set; }
        [InverseProperty("Attribute")]
        public virtual ICollection<AttributeValueText> AttributeValueText { get; set; }
        [InverseProperty("Attribute")]
        public virtual ICollection<AttributeValueVarchars> AttributeValueVarchars { get; set; }
    }
}
