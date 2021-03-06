using SupplyesOfProducts;
using SupplyesOfProducts.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyesOfProducts.Models
{
    /* Класс с описанием модели таблицы Products (реализует интерфейс IValidateModel)
         * Поля:
         *      Id - ид поставки
         *      Name - название поставки
         *      FixPrice - цена поставки по умолчанию
         *      FixWeight - вес поставки по умолчанию
         *      Error - ошибка в валдации модели
         *      ProductPrice - список цен поставок
         *      
         * Методы:
         *      ValidateModel - валидация модели
    */
    public class Products: IValidateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? FixPrice { get; set; }
        public double FixWeight { get; set; }

        public int ProviderId { get; set; }
        
        [ForeignKey("ProviderId")]
        public Providers Provider { get; set; }
        public List<ProductPrices> ProductPrice { get; set; }
       
        [NotMapped]
        public string Error { get; set; }
        public Products()
        {
        }

        public Products(Providers provider, string productName, decimal FixPrice, double FixWeight)
        {
            this.Provider = null;
            this.ProviderId = provider.Id;
            this.Name = productName;
            this.FixPrice = FixPrice;
            this.FixWeight = FixWeight;
            ProductPrice = new List<ProductPrices>();
        }

        public bool ValidateModel() 
        {
            Error = "";

            if (String.IsNullOrEmpty(Name))
                Error = "Должно быть заполнено название вида продукции";

            if (Provider is null)
                Error = "Должен быть заполнен подрядчик";

            if (FixPrice is null)
                Error = "Должна быть заполнена цена по умолчанию";

            if (FixWeight == 0)
                Error = "Должен быть заполнен вес по умолчанию";

            return string.IsNullOrEmpty(Error);
        }
    }
}

