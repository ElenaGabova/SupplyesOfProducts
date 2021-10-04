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
using SupplyesOfProducts.Classes;
using SupplyesOfProducts.Models;

namespace SupplyesOfProducts.Views
{

    /* Класс с методами для работы с окном CreateProductsWindow
    * Поля:
    *      product - текущий продукт
    *      productsList - список всех продуктов
    *      providersList - список всех поставщиков 
    *      grid         - компонент для вывода списка продуктов из БД
    *      isNewModel   - флаг плказывает, форма для создания новой записи или изменения текущей
    *      
    * Методы:
    *      CreateProduct_Click - создание или изменение продукта
    *      ValidateModel      - проверка валидации входных данных
    *      SetMessageText     - установка сообщения об ошибке, или изменении данных
    *      Cancel_Click       - закрытие формы по кнопке отмена
    *      Window_Closed      - закрытие формы
    */

    public partial class CreateProductsWindow : Window
    {
        Products product = new Products();
        ProductsList productsList = new ProductsList();
        ProvidersList providersList = new ProvidersList();
        DataGrid grid;
        bool isNewModel = true;

        public CreateProductsWindow()
        {
            InitializeComponent();

            this.DataContext = product;
            providersBox.ItemsSource = providersList.Providers;
        }

        public CreateProductsWindow(Products product, DataGrid grid)
        {  
            InitializeComponent();  
            
            this.DataContext = product;
            this.product     = product;
            this.grid = grid;
            isNewModel   = false;

            providersBox.ItemsSource   = providersList.Providers;
            providersBox.SelectedValue = providersList.Providers.Where(p => p.Id == product.ProviderId).First();
        }

        private void CreateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateModel(sender, e))
            {
                double fixWeight;
                string FixWeightText = FixWeight.Text;
                FixWeightText = FixWeightText.Replace('.', ',');

                bool weightSucess = double.TryParse(FixWeightText, out fixWeight);

                decimal fixPrice;
                bool priceSucess = decimal.TryParse(FixPrice.Text, out fixPrice);

                if (weightSucess && priceSucess)
                {
                    if (isNewModel)
                        productsList.AddProduct(providersBox.SelectedItem as Providers, ProductName.Text, fixPrice, fixWeight);
                    else
                        productsList.UpdateProduct(product.Id, providersBox.SelectedItem as Providers, ProductName.Text, fixPrice, fixWeight);
                }
                else
                {
                    string message = "Введите корректное значение ";
                    message += !weightSucess ? " веса" : " цены";

                    SetMessageText(sender, e, message, false);
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ShowProductsWindow window = new ShowProductsWindow();
            window.InitializeComponent();
        }

        private bool ValidateModel(object sender, RoutedEventArgs e)
        {
            product.ValidateModel();
            var error = product.Error;

            string goodMessge = isNewModel ? "Создан новый вид продукции" : "Изменен вид продукции";

            if (String.IsNullOrEmpty(error))
               SetMessageText(sender, e, goodMessge, true);
            else
               SetMessageText(sender, e, error, false);

            return String.IsNullOrEmpty(error);
        }

        private void SetMessageText(object sender, RoutedEventArgs e, string message, bool isInputCorrect = true)
        {
            var textColor  = isInputCorrect ? Colors.Green : Colors.Red;
            this.IsEnabled = !isInputCorrect;

            infoTextBlock.Text = message;
            infoTextBlock.Foreground = new SolidColorBrush(textColor);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            grid.ItemsSource = productsList.Products;
        }
    }
}
