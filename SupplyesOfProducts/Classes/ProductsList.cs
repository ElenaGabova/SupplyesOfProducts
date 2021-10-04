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
    /* Класс с методами для работы с таблицей Products
    * Поля:
    *      Products - список поставок
    *      db - контекст базы данных для работы с таблицей Products
    * 
    * Методы:
    *      AddProduct    - добавление продукта
    *      UpdateProduct - обновление продукта
    *      DeleteProduct - удаление продукта (с проверкой связанных данных)
    */

    class ProductsList 
    {
        public ObservableCollection<Products> Products { get; set; }
        private ApplicationContext db { get; set; } = new ApplicationContext();

        public ProductsList()
        {
            Products = new ObservableCollection<Products>();

            foreach (var item in db.Products.Include(p => p.Provider))
                Products.Add(item);
        }

        public void AddProduct(Providers provider, string productName, decimal FixPrice, double FixWeight)
        {
            Products product = new Products(provider, productName, FixPrice, FixWeight);
            Products.Add(product);
            db.Products.Add(product);
            db.SaveChanges();
        }
        public void UpdateProduct(int productID, Providers provider, string productName, decimal FixPrice, double FixWeight)
        {
            if (productID > 0)
            {
                Products product = db.Products.Find(productID);
                product.ProviderId = provider.Id;
                product.Name = productName;
                product.FixPrice = FixPrice;
                product.FixWeight = FixWeight;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public int DeleteProduct(int productID)
        {
            try 
            { 
                if (productID > 0)
                {
                    Products product = db.Products.Find(productID);
                    db.Products.Remove(product);
                    db.SaveChanges();
                    Products.Remove(product); 
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
