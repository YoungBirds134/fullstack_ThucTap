using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class ActionInFunctions
    {
        [Key]
        [StringLength(50)]
        public string FunctionId { get; set; }
        [Key]
        [StringLength(50)]
        public string ActionId { get; set; }

        [ForeignKey(nameof(ActionId))]
        [InverseProperty(nameof(Actions.ActionInFunctions))]
        public virtual Actions Action { get; set; }
        [ForeignKey(nameof(FunctionId))]
        [InverseProperty(nameof(Functions.ActionInFunctions))]
        public virtual Functions Function { get; set; }
    }
}
