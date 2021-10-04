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
    /* Класс с методами для работы с окном ShowProductPricesWindow     
     * Поля:
     *      productPricesList - список цен на поставки за период
     * Методы:
     *      CreateProductPrice_Click - показ окна для добавления цены на поставку
     *      UpdateProductPrice_Click - показ окна для обновления цены на поставку
     *      DeleteProductPrice_Click - удаление цены на поставку
     */

    public partial class ShowProductPricesWindow : Window
    {
        ProductPricesList productPricesList = new ProductPricesList();
        public ShowProductPricesWindow()
        {
            InitializeComponent();
            productPricesGrid.ItemsSource = productPricesList.ProductPrices;
        }


        private void CreateProductPrice_Click(object sender, RoutedEventArgs e)
        {
            CreateProductPricesWindow window = new CreateProductPricesWindow();
            window.Show();
        }

        private void UpdateProductPrice_Click(object sender, RoutedEventArgs e)
        {
            if (productPricesGrid.SelectedIndex > 0)
            {
                var productPrice = productPricesGrid.SelectedItem as ProductPrices;
                CreateProductPricesWindow window = new CreateProductPricesWindow(productPrice, productPricesGrid);
                window.Show();
            }
        }

        private void DeleteProductPrice_Click(object sender, RoutedEventArgs e)
        {
            if (productPricesGrid.SelectedIndex > 0)
            {            
                var productPrice = productPricesGrid.SelectedItem as ProductPrices;
                int result = productPricesList.DeleteProductPrice(productPrice.Id);
            }

        }
    }
}
