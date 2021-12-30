using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class OrderDetails
    {
        [Key]
        public int ProductId { get; set; }
        [Key]
        public int OrderId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(Orders.OrderDetails))]
        public virtual Orders Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Products.OrderDetails))]
        public virtual Products Product { get; set; }
    }
}
