using SupplyesOfProducts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyesOfProducts.Models
{
    /* Класс с описанием модели таблицы ProductPrices (реализует интерфейс IValidateModel)
         * Поля:
         *      Id - ид цены на поставку
         *      ProductId - ид продукта
         *      Product   - продукт
         *      DateStart - дата начала действия цены на поставку
         *      DateEnd   - дата конца действия цены на поставку
         *      Price - цена поставки за период
         *      Weight - вес для расчета цены поставки за период
         *      Error - ошибка в валдации модели
         *      Products - список цен поставок
         *      
         * Методы:
         *      ValidateModel - валидация модели
    */

    public class ProductPrices: IValidateModel
    {
        [Key] 
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public decimal? Price { get; set; }
        public double? Weight { get; set; }
        
        [NotMapped]
        public string Error { get; set; }

        public ProductPrices()
        {
        }

        public ProductPrices(Products product, DateTime DateStart, DateTime DateEnd, decimal Price,  double Weight)
        {
            this.Product = null;
            this.ProductId = product.Id;
            this.DateStart = DateStart;
            this.DateEnd   = DateEnd;
            this.Price     = Price;
            this.Weight    = Weight;
        }

        public bool ValidateModel()
        {
            Error = "";

            if (Product is null)
                Error = "Должен быть заполнен вид продукции";

            if (DateStart is null)
                Error = "Должна быть заполнена дата начала поставки";

            if (DateEnd is null)
                Error = "Должна быть заполнена дата конца поставки";

            if (Weight == 0 || Weight is null)
                Error = "Должен быть вес продукции";

            if (Price == 0 || Price is null)
                Error = "Должна быть заполнена цена продукции";

            return string.IsNullOrEmpty(Error);
        }
    }
}

