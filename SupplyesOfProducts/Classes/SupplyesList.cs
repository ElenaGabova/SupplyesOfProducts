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
    /* Класс с методами для работы с таблицей Supplyes
     * Поля:
     *      Providers - список поставок
     *      db - контекст базы данных для работы с таблицей Supplyes
     * 
     * Методы:
     *      AddSupply    - добавление поставки
     *      UpdateSupply - обновление поставки
     *      DeleteSupply - удаление поставки (с проверкой связанных данных)
     */

    class SupplyesList
    {
        public ObservableCollection<Supplyes> Supplyes { get; set; }

        private ApplicationContext db { get; set; } = new ApplicationContext();

        public SupplyesList()
        {
            Supplyes = new ObservableCollection<Supplyes>();
            foreach (var item in db.Supplyes.Include(s => s.Product))
            {
                Supplyes.Add(item);
            }
        }

        public void AddSupply(Products product, DateTime DateStart, double Weight)
        {
            Supplyes supply = new Supplyes(product, DateStart, Weight);   
            Supplyes.Add(supply);
            db.Supplyes.Add(supply);
            db.SaveChanges();
            supply.CalculatePrice();
        }

        public void UpdateSupply(int index, Products product, DateTime DateStart, double Weight)
        {
            Supplyes[index].ProductId = product.Id;
            Supplyes[index].DateStart = DateStart;
            Supplyes[index].Weight = Weight;
            db.Entry(Supplyes[index]).State = EntityState.Modified;
            db.SaveChanges();
            Supplyes[index].CalculatePrice();
        }

        public int DeleteSupply(int supplyID)
        {
            try
            {
                if (supplyID > 0)
                {
                    Supplyes supply = db.Supplyes.Find(supplyID);
                    db.Supplyes.Remove(supply);
                    db.SaveChanges();
                    Supplyes.Remove(supply);
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
