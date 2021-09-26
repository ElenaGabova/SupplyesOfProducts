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
    /// Логика взаимодействия для CreateProvidersWindow.xaml
    /// </summary>
    public partial class CreateProvidersWindow : Window
    {
        public Providers provider;
        ProvidersList providersList = new ProvidersList();
        bool isNewModel = true;
        int providerIndex = 0;

        public CreateProvidersWindow()
        {
            provider = new Providers();
            this.DataContext = provider;
            InitializeComponent();
        }
        public CreateProvidersWindow(Providers p, int index)
        {
            provider = p;
            this.DataContext = provider;
            isNewModel = false;
            providerIndex = index;
            InitializeComponent();
        }

        private void CreateProvider_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateModel(sender, e))
            {
                if (isNewModel)
                    providersList.AddProvider(ProviderName.Text);
                else
                    providersList.UpdateProvider(providerIndex, ProviderName.Text);
            }
                
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateModel(object sender, RoutedEventArgs e)
        {
            var textColor = Colors.Red;
            provider.ValidateModel();
            var error = provider.Error;

            if (String.IsNullOrEmpty(error))
            {
                textColor = Colors.Green;
                this.IsEnabled = false;
            }
            string goodMessge = isNewModel ? "Создан новый подрядчик" : "Изменен подрядчик";

            infoTextBlock.Text = String.IsNullOrEmpty(error) ? goodMessge : error;
            infoTextBlock.Foreground = new SolidColorBrush(textColor);
            return String.IsNullOrEmpty(error);
        }
     
    }
}
