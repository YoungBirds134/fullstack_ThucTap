using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class ProductTranslations
    {
        [Key]
        public int ProductId { get; set; }
        [Key]
        [StringLength(50)]
        public string LanguageId { get; set; }
        [Column(TypeName = "ntext")]
        public string Content { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string SeoDescription { get; set; }
        [StringLength(255)]
        public string SeoAlias { get; set; }
        [StringLength(255)]
        public string SeoTitle { get; set; }
        [StringLength(255)]
        public string SeoKeyword { get; set; }

        [ForeignKey(nameof(LanguageId))]
        [InverseProperty(nameof(Languages.ProductTranslations))]
        public virtual Languages Language { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Products.ProductTranslations))]
        public virtual Products Product { get; set; }
    }
}
