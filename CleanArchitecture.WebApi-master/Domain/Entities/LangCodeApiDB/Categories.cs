using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Categories
    {
        public Categories()
        {
            ProductInCategories = new HashSet<ProductInCategories>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string SeoAlias { get; set; }
        [StringLength(255)]
        public string SeoTitle { get; set; }
        [StringLength(255)]
        public string SeoKeyword { get; set; }
        [StringLength(255)]
        public string SeoDescription { get; set; }
        public int? ParentId { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<ProductInCategories> ProductInCategories { get; set; }
    }
}
