using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class ProductInCategories
    {
        [Key]
        public int ProductId { get; set; }
        [Key]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Categories.ProductInCategories))]
        public virtual Categories Category { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Products.ProductInCategories))]
        public virtual Products Product { get; set; }
    }
}
