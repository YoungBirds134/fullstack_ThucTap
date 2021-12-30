using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Actions
    {
        public Actions()
        {
            ActionInFunctions = new HashSet<ActionInFunctions>();
        }

        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("Action")]
        public virtual ICollection<ActionInFunctions> ActionInFunctions { get; set; }
    }
}
