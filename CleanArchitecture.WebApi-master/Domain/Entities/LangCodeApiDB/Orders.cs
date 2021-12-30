using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Contexts
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(255)]
        public string CustomerAddress { get; set; }
        [Required]
        [StringLength(255)]
        public string CustomerEmail { get; set; }
        [Required]
        [StringLength(20)]
        public string CustomerPhone { get; set; }
        [Required]
        [StringLength(255)]
        public string CustomerNote { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }

        [InverseProperty("Order")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
