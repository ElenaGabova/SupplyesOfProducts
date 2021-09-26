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
                if (isNewModel)
                    productsList.AddProduct(providersBox.SelectedItem as Providers, ProductName.Text, decimal.Parse(FixPrice.Text), int.Parse(FixWeight.Text));
                else
                    productsList.UpdateProduct(productIndex, providersBox.SelectedItem as Providers, ProductName.Text, decimal.Parse(FixPrice.Text), int.Parse(FixWeight.Text));
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
