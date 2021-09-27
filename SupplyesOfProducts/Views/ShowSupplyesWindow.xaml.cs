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
    /// Логика взаимодействия для ShowSupplyesWindow.xaml
    /// </summary>
    public partial class ShowSupplyesWindow : Window
    {
        Supplyes supply = new Supplyes();
        SupplyesList supplyesList = new SupplyesList();
        ProductsList productsList = new ProductsList();

        public ShowSupplyesWindow()
        {
            InitializeComponent(); 
            supplyesGrid.ItemsSource = supplyesList.Supplyes;
            decimal? allSum = 0;

            foreach (var supplyItem in supplyesList.Supplyes)
                allSum += supplyItem.CalculatePrice();

            AllSum.Text = allSum.ToString();
        }

        private void CreateSupply_Click(object sender, RoutedEventArgs e)
        {
            CreateSupplyesWindow window = new CreateSupplyesWindow();
            window.Show();
        }

        private void UpdateSupply_Click(object sender, RoutedEventArgs e)
        {
            var supply = supplyesGrid.SelectedItem as Supplyes;
            CreateSupplyesWindow window = new CreateSupplyesWindow(supply, supplyesGrid.SelectedIndex);
            window.Show();
        }
        private void DeleteSupply_Click(object sender, RoutedEventArgs e)
        {
            var product = supplyesGrid.SelectedItem as Supplyes;
            int result = supplyesList.DeleteSupply(supply);
        }
    }
}
