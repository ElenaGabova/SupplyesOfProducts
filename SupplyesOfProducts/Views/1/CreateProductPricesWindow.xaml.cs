using SupplyesOfProducts.Classes;
using SupplyesOfProducts.Classes.SupplyesOfProducts.Classes;
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
    /// <summary>
    /// Логика взаимодействия для CreateProductPricesWindow.xaml
    /// </summary>
    public partial class CreateProductPricesWindow : Window
    {
        /* Класс с методами для работы с окном CreateProductPricesWindow
        * Поля:
        *      productPrice - текущая цена поставки
        *      productPriceList - список всех цен поставок
        *      productsList - список всех продуктов
        *      grid         - компонент для вывода списка цен поставок из БД
        *      isNewModel   - флаг плказывает, форма для создания новой записи или изменения текущей
        *      
        * Методы:
        *      CreateProductPrice_Click - создание или изменение цены поставки
        *      ValidateModel      - проверка валидации входных данных
        *      SetMessageText     - установка сообщения об ошибке, или изменении данных
        *      Cancel_Click       - закрытие формы по кнопке отмена
        *      Window_Closed      - закрытие формы
        */

        ProductPrices productPrice = new ProductPrices();
        ProductPricesList productPriceList = new ProductPricesList();
        ProductsList productsList = new ProductsList();
        DataGrid grid;
        bool isNewModel = true;

        public CreateProductPricesWindow()
        {
            InitializeComponent();

            this.DataContext = productPrice;
            productsBox.ItemsSource = productsList.Products;
        }
        public CreateProductPricesWindow(ProductPrices productPrice, DataGrid grid)
        {
            InitializeComponent();
            this.productPrice = productPrice;
            this.DataContext = productPrice;
            this.grid = grid;
            isNewModel = false;

            productsBox.ItemsSource = productsList.Products;
            productsBox.SelectedValue = productsList.Products.Where(p => p.Id == productPrice.ProductId).First();
            DateStart.Text = productPrice.DateStart.ToString();
            DateEnd.Text = productPrice.DateEnd.ToString();
        }

        private void CreateProductPrice_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateModel(sender, e))
            {
                double weight;
                string weightText = Weight.Text;
                weightText = weightText.Replace('.', ',');
                bool weightSucess = double.TryParse(weightText, out weight);

                decimal price;
                string priceText = Price.Text;
                priceText = priceText.Replace('.', ',');
                bool priceSucess = decimal.TryParse(priceText, out price);

                if (weightSucess && priceSucess)
                {
                    if (isNewModel)
                        productPriceList.AddProductPrice(productsBox.SelectedItem as Products,
                                                         DateTime.Parse(DateStart.Text),
                                                         DateTime.Parse(DateEnd.Text),
                                                         price,
                                                         weight);
                    else
                        productPriceList.UpdateProductPrice(productPrice.Id,
                                                             productsBox.SelectedItem as Products,
                                                             DateTime.Parse(DateStart.Text),
                                                             DateTime.Parse(DateEnd.Text),
                                                             price,
                                                             weight);
                }
                else
                {
                    string message = "Введите корректное значение ";
                    message += !weightSucess ? " веса" : " цены";

                    SetMessageText(sender, e, message, false);
                }
            }
        }
        private bool ValidateModel(object sender, RoutedEventArgs e)
        {
            productPrice.Product = productsBox.SelectedItem as Products;
            productPrice.DateStart = DateStart.SelectedDate;
            productPrice.DateEnd   = DateEnd.SelectedDate;

            productPrice.ValidateModel();
            var error = productPrice.Error;

            string goodMessge = isNewModel ? "Создана новая цена поставки" : "Изменена цена поставки";

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
            grid.ItemsSource = productPriceList.ProductPrices;
        }
    }
}
