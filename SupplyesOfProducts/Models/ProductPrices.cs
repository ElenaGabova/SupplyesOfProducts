using SupplyesOfProducts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyesOfProducts.Models
{
    public class ProductPrices: IValidateModel
    {
        [Key] 
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Providers Product { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal Price { get; set; }
        public double Weight { get; set; }
        public string Error { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool ValidateModel()
        {
            throw new NotImplementedException();
        }
    }
}

