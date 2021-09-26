using SupplyesOfProducts.Database;
using SupplyesOfProducts.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SupplyesOfProducts.Models
{
    public class Providers : IValidateModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Name { get; set; }
    
        public ICollection<Products> Products { get; set; }
        
        [NotMapped]
        public string Error { get; set; }

        public Providers()
        {
        }

        public Providers(string ProviderName)
        {
            this.Name = ProviderName;
        }

        public bool ValidateModel()
        {
            Error = "";

            if (String.IsNullOrEmpty(Name))
                Error = "Должен быть заполнен подрядчик";

            return string.IsNullOrEmpty(Error);
        }

    }

}

