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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SupplyesOfProducts.Database;
using SupplyesOfProducts.Models;
using SupplyesOfProducts.Views;

namespace SupplyesOfProducts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddProvider_Click(object sender, RoutedEventArgs e)
        {
            CreateProvidersWindow window = new CreateProvidersWindow();
            window.Show();
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            CreateProductsWindow window = new CreateProductsWindow();
            window.Show();
        }
    

    }
}
