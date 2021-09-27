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
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class CreateProductsWindow : Window
    {
        Products product = new Products();
        ProductsList productsList = new ProductsList();
        ProvidersList providersList = new ProvidersList();

        bool isNewModel = true;
        int productIndex = 0;

        public CreateProductsWindow()
        {
            InitializeComponent();

            this.DataContext = product;
            providersBox.ItemsSource = providersList.Providers;
        }

        public CreateProductsWindow(Products p, int index)
        {  
            InitializeComponent();
            product = p;
            this.DataContext = product;
            providersBox.ItemsSource = providersList.Providers;
            providersBox.SelectedValue = p.Provider;
            isNewModel = false;
            productIndex = index;
          
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
                        productsList.UpdateProduct(productIndex, providersBox.SelectedItem as Providers, ProductName.Text, fixPrice, fixWeight);
                }
                else
                {
                    infoTextBlock.Text = "Введите корректное значение ";
                    infoTextBlock.Text += !weightSucess ? " веса" : " цены";
                    infoTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                    this.IsEnabled = true;
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
            var textColor = Colors.Red;
            product.Provider = (Providers) providersBox.SelectedItem;
            product.ValidateModel();
            var error = product.Error;

            if (String.IsNullOrEmpty(error))
            {
                textColor = Colors.Green;
                this.IsEnabled = false;
            }

            string goodMessge = isNewModel ? "Создан новый вид продукции" : "Изменен вид продукции";

            infoTextBlock.Text = error;
            infoTextBlock.Foreground = new SolidColorBrush(textColor);
            return String.IsNullOrEmpty(error);
        }
    }
}
