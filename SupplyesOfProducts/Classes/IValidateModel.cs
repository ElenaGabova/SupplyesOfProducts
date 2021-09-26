using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyesOfProducts.Models
{
    interface IValidateModel
    {
        string Error { get; set; }
        bool ValidateModel();
    }
}
