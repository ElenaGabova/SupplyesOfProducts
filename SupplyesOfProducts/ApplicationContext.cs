using SupplyesOfProducts.Models;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace SupplyesOfProducts.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Providers> Providers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductPrices> ProductPrices { get; set; }
        public DbSet<Supplyes> Supplyes { get; set; }
        public ApplicationContext()
    : base("DbConnection")
        { }
    }
}

