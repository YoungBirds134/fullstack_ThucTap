using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Languages
    {
        public Languages()
        {
            ProductTranslations = new HashSet<ProductTranslations>();
        }

        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDefault { get; set; }
        public int? SortOrder { get; set; }

        [InverseProperty("Language")]
        public virtual ICollection<ProductTranslations> ProductTranslations { get; set; }
    }
}
