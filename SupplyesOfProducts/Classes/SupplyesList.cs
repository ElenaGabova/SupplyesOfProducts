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
    class SupplyesList
    {
        private ObservableCollection<Supplyes> supplyes;
        public ObservableCollection<Supplyes> Supplyes
        {
            get
            {
                return supplyes;
            }

            private set
            {
                supplyes = value;
            }
        }

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

        public int DeleteSupply(Supplyes supply)
        {
            // try
            //{
            Supplyes.Remove(supply);
            var entry = db.Entry(supply);
            entry.State = EntityState.Added;
            //if (entry.State == EntityState.Detached)
            //    db.Supplyes.Attach(supply);

            db.Supplyes.Remove(supply);
            db.Configuration.ValidateOnSaveEnabled = false;
            // db.Database.ExecuteSqlCommand("DELETE FROM Supplyes where ID = " + supply.Id);
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
            return 1;
            //}
            //catch
            //{
            //    return 0;
            //}
        }
    }
}
