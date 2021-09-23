using SupplyesOfProducts;
using System;

namespace SupplyesOfProducts.Models
{
    public class ProductPrices
    {
        public int Id { get; set; }
        public Products Product { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal Price { get; set; }
        public double Weight { get; set; }
    }
}

