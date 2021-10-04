using SupplyesOfProducts.Classes;
using SupplyesOfProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SupplyesOfProducts.Views
{
    /* Класс с методами для работы с окном CreateSupplyesWindow
    * Поля:
    *      supply - текущая поставка
    *      supplyesList - список всех поставок
    *      productsList - список всех продуктов
    *      grid         - компонент для вывода списка поставок из БД
    *      isNewModel   - флаг плказывает, форма для создания новой записи или изменения текущей
    *      
    * Методы:
    *      CreateSupply_Click - создание или изменение поставки
    *      ValidateModel      - проверка валидации входных данных
    *      SetMessageText     - установка сообщения об ошибке, или изменении данных
    *      Cancel_Click       - закрытие формы по кнопке отмена
    *      Window_Closed      - закрытие формы
    */

    public partial class CreateSupplyesWindow : Window
    {
        Supplyes supply = new Supplyes();
        SupplyesList supplyesList = new SupplyesList();
        ProductsList productsList = new ProductsList();
        DataGrid grid;
        bool isNewModel = true;
        
        public CreateSupplyesWindow()
        {
            InitializeComponent();

            this.DataContext = supply;
            productsBox.ItemsSource = productsList.Products;
        }

        public CreateSupplyesWindow(Supplyes supply, DataGrid grid)
        {
            InitializeComponent();
            this.DataContext = supply;
            this.grid = grid;
            isNewModel = false;

            productsBox.ItemsSource = productsList.Products;
            productsBox.SelectedValue = supplyesList.Supplyes.Where(p => p.Id == supply.ProductId).First();;
            DateStart.Text = supply.DateStart.ToString();
        }

        private void CreateSupply_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateModel(sender, e))
            {
                double WeightValue;
                string WeightText = Weight.Text;
                WeightText = WeightText.Replace('.', ',');

                bool weightSucess = double.TryParse(WeightText, out WeightValue);

                if (weightSucess)
                {
                    if (isNewModel)
                        supplyesList.AddSupply(productsBox.SelectedItem as Products, DateTime.Parse(DateStart.Text), WeightValue);
                    else
                        supplyesList.UpdateSupply(supply.Id, productsBox.SelectedItem as Products, DateTime.Parse(DateStart.Text), WeightValue);
                }
                else
                {
                    string message = "Введите корректное значение веса";
                    SetMessageText(sender, e, message, false);
                }
            }
        }

        private bool ValidateModel(object sender, RoutedEventArgs e)
        {
            supply.ValidateModel();
            var error = supply.Error;

            string goodMessge = isNewModel ? "Создана новая поставка" : "Изменена поставка";

            if (String.IsNullOrEmpty(error))
                SetMessageText(sender, e, goodMessge, true);
            else
                SetMessageText(sender, e, error, false);

            return String.IsNullOrEmpty(error);
        }

        private void SetMessageText(object sender, RoutedEventArgs e, string message, bool isInputCorrect = true)
        {
            var textColor = isInputCorrect ? Colors.Green : Colors.Red;
            this.IsEnabled = !isInputCorrect;

            infoTextBlock.Text = message;
            infoTextBlock.Foreground = new SolidColorBrush(textColor);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            grid.ItemsSource = supplyesList.Supplyes;
        }
    }
}
