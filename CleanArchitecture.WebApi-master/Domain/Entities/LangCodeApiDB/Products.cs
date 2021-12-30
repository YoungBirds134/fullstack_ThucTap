using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Products
    {
        public Products()
        {
            AttributeValueDateTimes = new HashSet<AttributeValueDateTimes>();
            AttributeValueDecimals = new HashSet<AttributeValueDecimals>();
            AttributeValueInts = new HashSet<AttributeValueInts>();
            AttributeValueText = new HashSet<AttributeValueText>();
            AttributeValueVarchars = new HashSet<AttributeValueVarchars>();
            OrderDetails = new HashSet<OrderDetails>();
            ProductInCategories = new HashSet<ProductInCategories>();
            ProductTranslations = new HashSet<ProductTranslations>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Sku { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; }
        public string ImageList { get; set; }
        public int? ViewCount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public int? RateTotal { get; set; }
        public int? RateCount { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<AttributeValueDateTimes> AttributeValueDateTimes { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<AttributeValueDecimals> AttributeValueDecimals { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<AttributeValueInts> AttributeValueInts { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<AttributeValueText> AttributeValueText { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<AttributeValueVarchars> AttributeValueVarchars { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductInCategories> ProductInCategories { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductTranslations> ProductTranslations { get; set; }
    }
}
