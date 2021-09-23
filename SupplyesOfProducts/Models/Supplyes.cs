using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyesOfProducts.Models
{
    public class Supplyes
    {
        public int Id { get; set; }
        public Products Product { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public double Weight { get; set; }
    }
}

