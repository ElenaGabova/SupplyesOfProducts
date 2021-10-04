using SupplyesOfProducts.Classes;
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
    /* Класс с методами для работы с окном CreateProvidersWindow
     * Поля:
     *      provider - текущий поставщик
     *      providersList - список всех поставщиков 
     *      grid         - компонент для вывода списка поставщиков из БД
     *      isNewModel   - флаг плказывает, форма для создания новой записи или изменения текущей
     *      
     * Методы:
     *      CreateProvider_Click - создание или изменение поставки
     *      ValidateModel      - проверка валидации входных данных
     *      SetMessageText     - установка сообщения об ошибке, или изменении данных
     *      Cancel_Click       - закрытие формы по кнопке отмена
     *      Window_Closed      - закрытие формы
     */

    public partial class CreateProvidersWindow : Window
    {
        Providers provider;
        ProvidersList providersList = new ProvidersList();   
        DataGrid grid;
        bool isNewModel = true;

        public CreateProvidersWindow()
        {   InitializeComponent();
           
            provider = new Providers();
            this.DataContext = provider;
        }

        public CreateProvidersWindow(Providers p, DataGrid grid)
        {
            provider = p;
            this.DataContext = provider;
            isNewModel = false;
            this.grid = grid;
            
            InitializeComponent();
        }

        private void CreateProvider_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateModel(sender, e))
            {
                if (isNewModel)
                    providersList.AddProvider(ProviderName.Text);
                else
                    providersList.UpdateProvider(provider.Id, ProviderName.Text);
            }
        }

        private bool ValidateModel(object sender, RoutedEventArgs e)
        {
            provider.ValidateModel();
            var error = provider.Error;

            string goodMessge = isNewModel ? "Создан новый подрядчик" : "Изменен подрядчик";

            if (String.IsNullOrEmpty(error))
                SetMessageText(sender, e, goodMessge, true);
            else
                SetMessageText(sender, e, error, false);
            return String.IsNullOrEmpty(error);
        }

        private void SetMessageText(object sender, RoutedEventArgs e, string message, bool isInputCorrect = true)
        {
            var textColor = isInputCorrect ? Colors.Green : Colors.Red;
            this.IsEnabled = !isInputCorrect;

            infoTextBlock.Text = message;
            infoTextBlock.Foreground = new SolidColorBrush(textColor);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            grid.ItemsSource = providersList.Providers;
        }
    }
}
