using SupplyesOfProducts;
using System.Collections.Generic;

namespace SupplyesOfProducts.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Providers Provider { get; set; }
        public List<ProductPrices> ProductPrice { get; set; }

    }
}

