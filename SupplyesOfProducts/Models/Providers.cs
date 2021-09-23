using SupplyesOfProducts.Database;
using SupplyesOfProducts.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SupplyesOfProducts.Models
{
    public class Providers: IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Products> ProductsList { get; set; }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (String.IsNullOrEmpty(Name))
                        {
                            error = "Должно быть заполнено имя подрядчика";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public Providers()
        {
        }

        public Providers(string ProviderName)
        {
            this.Name = ProviderName;
            ProductsList = new List<Products>();
        }

        public void AddProvider(string ProviderName)
        {
            var db = new ApplicationContext();
            Providers p = new Providers(ProviderName);        
            db.Providers.Add(p);
            db.SaveChanges();
        }

        public static ObservableCollection<Providers> GetProviders()
        {
            var providersList = new ObservableCollection<Providers>();

            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var item in db.Providers)
                    providersList.Add(item);
            }

            return providersList;
        }

    }

}

