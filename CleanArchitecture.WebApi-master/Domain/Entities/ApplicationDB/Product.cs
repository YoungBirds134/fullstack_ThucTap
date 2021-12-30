using System;
using System.Collections.Generic;


namespace Infrastructure.Persistence.Models
{
    public partial class Product
    {
      
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
      
        public decimal Rate { get; set; }
    }
}
