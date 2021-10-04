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
    /* Класс с методами для работы с окном ShowProductsWindow     
    * Поля:
    *      productsList - список поставок
    * Методы:
    *      CreateProduct_Click - показ окна для добавления поставки
    *      UpdateProduct_Click - показ окна для обновления поставки
    *      DeleteProduct_Click - удаление поставки
    */

    public partial class ShowProductsWindow : Window
    {
        ProductsList productsList = new ProductsList();

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
            if (productsGrid.SelectedIndex > 0)
            {
                var provider = productsGrid.SelectedItem as Products;
                CreateProductsWindow window = new CreateProductsWindow(provider, productsGrid);
                window.Show();

                productsGrid.ItemsSource = productsList.Products;
            }
        }
        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (productsGrid.SelectedIndex > 0)
            {
                var product = productsGrid.SelectedItem as Products;
                int result = productsList.DeleteProduct(product.Id);
                if (result == 0)
                    MessageBox.Show("Удаление невозможно! Есть связанные данные");
            }
        }
    }
}
