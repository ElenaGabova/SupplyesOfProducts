using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyesOfProducts.Models
{
    public class Supplyes : IValidateModel
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Providers Product { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public double Weight { get; set; }
        public string Error { get; set; }
        public bool ValidateModel()
        {
            Error = "";

            if (Product is null)
                Error = "Должен быть заполнен вид продукции";

            return string.IsNullOrEmpty(Error);
        }
    }
}

