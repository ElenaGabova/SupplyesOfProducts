﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyesOfProducts.Models
{
    public class Supplyes : IValidateModel
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Product { get; set; }
        public DateTime? DateStart { get; set; }
        public double ? Weight { get; set; }

        [NotMapped]
        public decimal? Price { get; set; }

        [NotMapped]
        public string Error { get; set; }


        public bool ValidateModel()
        {
            Error = "";

            if (Product is null)
                Error = "Должен быть заполнен вид продукции";

            if (DateStart is null)
                Error = "Должна быть заполнена дата поставки";

            if (Weight == 0 || Weight is null)
                Error = "Должен быть вес продукции";

            return string.IsNullOrEmpty(Error);
        }

        public Supplyes() { }
        public Supplyes(Products product, DateTime DateStart, double Weight)
        {
            this.Product = null;
            this.ProductId = product.Id;
            this.DateStart = DateStart;
            this.Weight = Weight;
        }

        public decimal? CalculatePrice()
        {
            if (Product is null)
                return 0;
            return ((decimal)Weight * Product.FixPrice) / (decimal)Product.FixWeight;
        } 
    }
}

