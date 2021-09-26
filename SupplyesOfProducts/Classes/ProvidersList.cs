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
    public class ProvidersList
    {
        private ObservableCollection<Providers> providers;
        public  ObservableCollection<Providers> Providers
        {
            get
            {
                return providers;
            }

            private set
            {
                providers = value;
            }
        }

        private ApplicationContext db { get; set; } = new ApplicationContext();

        public ProvidersList()
        {
            Providers = new ObservableCollection<Providers>();
            foreach (var item in db.Providers)
                Providers.Add(item);
        }

        public void AddProvider(string ProviderName)
        {
            Providers provider = new Providers(ProviderName);
            Providers.Add(provider);
            db.Providers.Add(provider);
            db.SaveChanges();
        }
        public void UpdateProvider(int index, string ProviderName)
        {
            Providers[index].Name = ProviderName;
            db.Entry(Providers[index]).State = EntityState.Modified;
            db.SaveChanges();
               
        }
        public int DeleteProvider(Providers provider)
        {
            try
            {
                if (!(provider is null))
                {
                    Providers.Remove(provider);
                    var entry = db.Entry(provider);
                    if (entry.State == EntityState.Detached)
                        db.Providers.Attach(provider);

                    db.Providers.Remove(provider);
                    db.SaveChanges();
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
