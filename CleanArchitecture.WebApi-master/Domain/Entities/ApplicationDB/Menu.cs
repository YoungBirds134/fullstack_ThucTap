using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Menu
    {
        
        public string id { get; set; }
      
        public string nameMenu { get; set; }

        public string parentID { get; set; }

        public bool? active { get; set; }

    }
}
