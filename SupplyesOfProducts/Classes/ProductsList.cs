using SupplyesOfProducts.Database;
using SupplyesOfProducts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyesOfProducts.Classes
{
    class ProductsList {

        private ObservableCollection<Products> products;
        public ObservableCollection<Products> Products
        {
            get
            {
                return products;
            }

            private set
            {
                products = value;
            }
        }

        private ApplicationContext db { get; set; } = new ApplicationContext();

        public ProductsList()
        {
            Products = new ObservableCollection<Products>();
            foreach (var item in db.Products.Include(p => p.Provider))
            {
                Products.Add(item);
            }
        }

        public void AddProduct(Providers provider, string productName, decimal FixPrice, int FixWeight)
        {
            Products product = new Products(provider, productName, FixPrice, FixWeight);
            Products.Add(product);
            db.Products.Add(product);
            db.SaveChanges();
        }
        public void UpdateProduct(int index, Providers provider, string productName, decimal FixPrice, int FixWeight)
        {
            Products[index].ProviderId = provider.Id;
            Products[index].Name = productName;
            Products[index].FixPrice = FixPrice;
            Products[index].FixWeight = FixWeight;
            db.Entry(Products[index]).State = EntityState.Modified;
            db.SaveChanges();

        }
        public int DeleteProduct(Products product)
        {
            try 
            { 

                Products.Remove(product);
                var entry = db.Entry(product);
                if (entry.State == EntityState.Detached)
                    db.Products.Attach(product);

                db.Products.Remove(product);
                db.SaveChanges();

                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
