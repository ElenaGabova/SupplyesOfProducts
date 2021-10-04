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
    namespace SupplyesOfProducts.Classes
    {
        /* Класс с методами для работы с таблицей ProductPrices
         * Поля:
         *      ProductPrices - список цен поставок за период
         *      db - контекст базы данных для работы с таблицей Supplyes
         * 
         * Методы:
         *      AddProductPrice    - добавление цены поставки
         *      UpdateProductPrice - обновление цены поставки
         *      DeleteProductPrice - удаление цены поставки (с проверкой связанных данных)
         */

        class ProductPricesList
        {
            public ObservableCollection<ProductPrices> ProductPrices { get; set; }

            private ApplicationContext db { get; set; } = new ApplicationContext();

            public ProductPricesList()
            {
               ProductPrices = new ObservableCollection<ProductPrices>();
               foreach (var item in db.ProductPrices.Include(p => p.Product))
                    ProductPrices.Add(item);
              
            }

            public void AddProductPrice(Products product, DateTime DateStart, DateTime DateEnd, decimal Price, double Weight)
            {
                ProductPrices productPrice = new ProductPrices(product, DateStart, DateEnd, Price, Weight);
                ProductPrices.Add(productPrice);
                db.ProductPrices.Add(productPrice);
                db.SaveChanges();
            }

            public void UpdateProductPrice(int productPriceId, Products product, DateTime DateStart, DateTime DateEnd, decimal Price, double Weight)
            {
                if (productPriceId > 0)
                {
                    ProductPrices productPrice = db.ProductPrices.Find(productPriceId);

                    productPrice.ProductId = product.Id;
                    productPrice.DateStart = DateStart;
                    productPrice.DateEnd = DateEnd;
                    productPrice.Price = Price;
                    productPrice.Weight = Weight;

                    db.Entry(productPrice).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            public int DeleteProductPrice(int productPriceId)
            {
                try
                {
                    if (productPriceId > 0)
                    {
                        ProductPrices productPrice = db.ProductPrices.Find(productPriceId);
                        db.ProductPrices.Remove(productPrice);
                        db.SaveChanges();
                        ProductPrices.Remove(productPrice);
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
}
