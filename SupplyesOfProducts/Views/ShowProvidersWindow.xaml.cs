using SupplyesOfProducts.Classes;
using SupplyesOfProducts.Database;
using SupplyesOfProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

    /* Класс с методами для работы с окном ShowProvidersWindow     
    * Поля:
    *      providersList - список поставщиков
    * Методы:
    *      CreateProvider_Click - показ окна для добавления поставщиков
    *      UpdateProvider_Click - показ окна для обновления поставщиков
    *      DeleteProvider_Click - удвление поставщика
    */

    public partial class ShowProvidersWindow : Window
    {
        ProvidersList providersList = new ProvidersList();

        public ShowProvidersWindow()
        {
            InitializeComponent();
            ProvidersGrid.ItemsSource = providersList.Providers;
        }

        private void CreateProvider_Click(object sender, RoutedEventArgs e)
        {
            CreateProvidersWindow window = new CreateProvidersWindow();
            window.Show();
        }

        private void UpdateProvider_Click(object sender, RoutedEventArgs e)
        {
            if (ProvidersGrid.SelectedIndex > 0)
            {
                var provider = ProvidersGrid.SelectedItem as Providers;
                CreateProvidersWindow window = new CreateProvidersWindow(provider, ProvidersGrid);
                window.Show();
            }
        }
        private void DeleteProvider_Click(object sender, RoutedEventArgs e)
        {
            if (ProvidersGrid.SelectedIndex > 0)
            {
                var provider = ProvidersGrid.SelectedItem as Providers;
                int result = providersList.DeleteProvider(provider.Id);
                if (result == 0)
                    MessageBox.Show("Удаление невозможно! Есть связанные данные");
            }
        }
    }
}
