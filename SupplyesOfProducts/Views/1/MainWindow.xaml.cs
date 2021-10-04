using System;
using System.Collections.Generic;
using System.Drawing;
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
using SupplyesOfProducts.Classes;
using SupplyesOfProducts.Database;

namespace SupplyesOfProducts.Views
{
    /* Класс с методами для работы с окном MainWindow    
     * Методы:
     *      ShowProvider_Click    - показ окна со списком подрядчиков
     *      ShowProduct_Click     - показ окна со списком продуктов
     *      ShowSupply_Click      - показ окна со списком поставок
     *      ShowExcelSupply_Click - показ окна для формирования Excel отчета по поставкам
     */

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowProvider_Click(object sender, RoutedEventArgs e)
        {
            ShowProvidersWindow window = new ShowProvidersWindow();
            window.Show();
        }
        private void ShowProduct_Click(object sender, RoutedEventArgs e)
        {
            ShowProductsWindow window = new ShowProductsWindow();
            window.Show();
        }
        private void ShowProductPrice_Click(object sender, RoutedEventArgs e)
        {
            ShowProductPricesWindow window = new ShowProductPricesWindow();
            window.Show();
        }

        private void ShowSupply_Click(object sender, RoutedEventArgs e)
        {
            ShowSupplyesWindow window = new ShowSupplyesWindow();
            window.Show();
        }

        private void ShowExcelSupply_Click(object sender, RoutedEventArgs e)
        {
            CreateDialogWindow dialog = new CreateDialogWindow();
            dialog.Show();
        }

    }
}
