using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class MenuDTO
    {
        public string id { get; set; }
        public string nameMenu { get; set; }
        public string parentID { get; set; }

        public string route { get; set; }
        public string nameComponent { get; set; }


        public bool active { get; set; }

     public   IEnumerable<MenuDTO> listMenu { get; set; }

    }
}
