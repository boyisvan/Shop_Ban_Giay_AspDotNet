using System;
using System.Collections.Generic;

namespace ShoeStore.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int? CategoryId { get; set; }
        public string? ImageUrl { get; set; }

        public virtual Category? Category { get; set; }
    }
}
