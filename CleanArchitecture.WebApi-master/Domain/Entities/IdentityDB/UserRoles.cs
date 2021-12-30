using System;
using System.Collections.Generic;


namespace Infrastructure.Identity.Contexts
{
    public partial class UserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

     
        public virtual Role Role { get; set; }
      
        public virtual User User { get; set; }
    }
}
