using System;
using System.Collections.Generic;

namespace ShoeStore.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? OrderStatus { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
