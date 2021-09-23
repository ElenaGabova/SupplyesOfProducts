using SupplyesOfProducts.Database;
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
        public CreateProvidersWindow()
        {
            InitializeComponent();
            var Tom = new Providers("");
            this.DataContext = Tom;

        }

        private void CreateProvider_Click(object sender, RoutedEventArgs e)
        {
            Providers providers = new Providers();
            providers.AddProvider(ProviderName.Text);
        }

        private void CreateValidation_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());
            var textColor = Colors.Red;
            infoTextBlock.Text = e.Error.ErrorContent.ToString();
            infoTextBlock.Foreground = new SolidColorBrush(textColor);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
