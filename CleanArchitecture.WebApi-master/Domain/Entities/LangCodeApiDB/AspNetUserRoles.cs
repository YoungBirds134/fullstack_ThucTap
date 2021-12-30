using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class AspNetUserRoles
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual AspNetRoles Role { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AspNetUsers User { get; set; }
    }
}
