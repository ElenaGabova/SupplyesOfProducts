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
    /// <summary>
    /// Логика взаимодействия для ShowProductsWindow.xaml
    /// </summary>
    public partial class ShowProductsWindow : Window
    {
        Products products = new Products();
        ProductsList productsList = new ProductsList();
        ProvidersList providerList = new ProvidersList();

        public ShowProductsWindow()
        {
            InitializeComponent();
            productsGrid.ItemsSource = productsList.Products;
        }

        private void CreateProduct_Click(object sender, RoutedEventArgs e)
        {
            CreateProductsWindow window = new CreateProductsWindow();
            window.Show();

            productsGrid.ItemsSource = productsList.Products;
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            var provider = productsGrid.SelectedItem as Products;
            CreateProductsWindow window = new CreateProductsWindow(provider, productsGrid.SelectedIndex);
            window.Show();

            productsGrid.ItemsSource = productsList.Products;
        }
        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = productsGrid.SelectedItem as Products;
            int result = productsList.DeleteProduct(product);
            if (result == 0)
                MessageBox.Show("Удаление невозможно! Есть связанные данные");
        }
    }
}
