using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Functions
    {
        public Functions()
        {
            ActionInFunctions = new HashSet<ActionInFunctions>();
        }

        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Url { get; set; }
        [StringLength(50)]
        public string ParentId { get; set; }
        public int? SortOrder { get; set; }
        [StringLength(50)]
        public string CssClass { get; set; }
        public bool IsActive { get; set; }

        [InverseProperty("Function")]
        public virtual ICollection<ActionInFunctions> ActionInFunctions { get; set; }
    }
}
