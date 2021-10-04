using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyesOfProducts.Models
{
    /* Интерфейс для проверки валидации IValidateModel
     * Поля:
     *      Error - ошибка
     *      
     * Методы:
     *      ValidateModel - валидация модели
     */

    interface IValidateModel
    {
        string Error { get; set; }
        bool ValidateModel();
    }
}
