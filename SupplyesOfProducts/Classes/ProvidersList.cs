using SupplyesOfProducts.Database;
using SupplyesOfProducts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyesOfProducts.Classes
{
    /* Класс с методами для работы с таблицей Providers
     * Поля:
     *      Providers - список подрядчиков
     *      db - контекст базы данных для работы с таблицей Providers
     * 
     * Методы:
     *      AddProvider    - добавление подрядчика
     *      UpdateProvider - обновление подрядчика
     *      DeleteProvider - удаление подрядчика (с проверкой связанных данных)
     */

    public class ProvidersList
    {
        public ObservableCollection<Providers> Providers { get; set; }
        private ApplicationContext  db { get; set; } = new ApplicationContext();

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

        public int DeleteProvider(int providerId)
        {
            try
            {
                if (providerId > 0)
                {
                    Providers provider = db.Providers.Find(providerId);
                    db.Providers.Remove(provider);
                    db.SaveChanges();
                    Providers.Remove(provider);
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
