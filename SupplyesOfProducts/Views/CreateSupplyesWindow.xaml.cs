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
    /// Логика взаимодействия для CreateSupplyesWindow.xaml
    /// </summary>
    public partial class CreateSupplyesWindow : Window
    {
        Supplyes supply = new Supplyes();
        SupplyesList supplyesList = new SupplyesList();
        ProductsList productsList = new ProductsList();

        bool isNewModel = true;
        int supplyIndex = 0;

        public CreateSupplyesWindow()
        {
            InitializeComponent();

            this.DataContext = supply;
            productsBox.ItemsSource = productsList.Products;
        }

        public CreateSupplyesWindow(Supplyes s, int index)
        {
            InitializeComponent();
            supply = s;

            this.DataContext = supply;
            productsBox.ItemsSource = productsList.Products;
            productsBox.SelectedValue = s.Product;
            DateStart.Text = s.DateStart.ToString();
            isNewModel = false;
            supplyIndex = index;

        }

        private void CreateSupply_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateModel(sender, e))
            {
                double WeightValue;
                string WeightText = Weight.Text;
                WeightText = WeightText.Replace('.', ',');

                bool weightSucess = double.TryParse(WeightText, out WeightValue);

                if (weightSucess)
                {
                    if (isNewModel)
                        supplyesList.AddSupply(productsBox.SelectedItem as Products, DateTime.Parse(DateStart.Text), WeightValue);
                    else
                        supplyesList.UpdateSupply(supplyIndex, productsBox.SelectedItem as Products, DateTime.Parse(DateStart.Text), WeightValue);
                }
                else
                {
                    infoTextBlock.Text = "Введите корректное значение веса";
                    infoTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                    this.IsEnabled = true;
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateModel(object sender, RoutedEventArgs e)
        {
            var textColor = Colors.Red;
            supply.Product = (Products)productsBox.SelectedItem;
            supply.DateStart = DateStart.SelectedDate;
            supply.ValidateModel();
            var error = supply.Error;

            if (String.IsNullOrEmpty(error))
            {
                textColor = Colors.Green;
                this.IsEnabled = false;
            }

            string goodMessge = isNewModel ? "Создана новая поставка" : "Изменена поставка";

            infoTextBlock.Text = error;
            infoTextBlock.Foreground = new SolidColorBrush(textColor);
            return String.IsNullOrEmpty(error);
        }
    }
}
