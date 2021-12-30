using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Permissions
    {
        [StringLength(50)]
        public string FunctionId { get; set; }
        [StringLength(50)]
        public string ActionId { get; set; }
        public Guid? RoleId { get; set; }

        [ForeignKey(nameof(ActionId))]
        public virtual Actions Action { get; set; }
        [ForeignKey(nameof(FunctionId))]
        public virtual Functions Function { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual AspNetRoles Role { get; set; }
    }
}
