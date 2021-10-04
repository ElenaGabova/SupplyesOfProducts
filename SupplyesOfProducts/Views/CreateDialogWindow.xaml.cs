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
using System.Windows.Shapes;
using SupplyesOfProducts.Classes;
using SupplyesOfProducts.Models;
using SupplyesOfProducts.Views;
using Excel = Microsoft.Office.Interop.Excel;

namespace SupplyesOfProducts.Views
{
    /* Класс с методами для работы с окном CreateDialogWindow     
    * Методы:
    *      ButtonClick - показ окна с запросом параметров для формирование отчета
    *      CreateExcelReport - формирование отчета
    */

    public partial class CreateDialogWindow : Window
    {
        public CreateDialogWindow()
        {
            InitializeComponent();
        }

        public void ButtonClick(object sender, RoutedEventArgs e)
        {

            if (DateStart.SelectedDate > DateEnd.SelectedDate || DateStart.SelectedDate is null || DateEnd.SelectedDate is null)
                MessageBox.Show("Введите корректный диапозон дат");
            else
                CreateExcelReport(DateStart.SelectedDate, DateEnd.SelectedDate);
        }

        public void CreateExcelReport(DateTime? DateStart, DateTime? DateEnd)
        {
            var supplyesList = new SupplyesList().Supplyes.Where(s => s.DateStart >= DateStart).Where(s => s.DateStart <= DateEnd);

            if (supplyesList.Count() > 0)
            {
                Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
                //Отобразить Excel
                ex.Visible = true;
                ex.SheetsInNewWorkbook = 1;
                Excel.Workbook workBook = ex.Workbooks.Add(Type.Missing);

                //Отключить отображение окон с сообщениями
                ex.DisplayAlerts = false;
                Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
                int j = 1;

                sheet.Cells[1, 1] = "Отчет по поставкам за период: c " + DateStart + " по " + DateEnd;
                sheet.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 5]].Merge();

                sheet.Columns[1].ColumnWidth = sheet.Columns[1].ColumnWidth * 2;

                //заполняем столбцы
                foreach (var nameColumn in new[] { "Вид продукции", "Дата поставки", "Вес единицы поставки", "Цена единицы поставки", "Цена поставки" })
                {
                    sheet.Cells[2, j] = nameColumn;
                    sheet.Columns[j].ColumnWidth = sheet.Columns[j].ColumnWidth * 2.5;
                    j++;
                }

                //вставляем данные
                int i = 3;
                foreach (var supply in supplyesList)
                {
                    sheet.Cells[i, 1] = supply.Product.Name;
                    sheet.Cells[i, 2] = supply.DateStart;
                    sheet.Cells[i, 3] = supply.Product.FixWeight;
                    sheet.Cells[i, 4] = supply.Product.FixPrice;
                    sheet.Cells[i, 5] = supply.CalculatePrice();
                    i++;
                }

                sheet.Cells[i, 2] = "Общий вес поставок";
                sheet.Cells[i, 3].Formulalocal = $"=СУММ(C3:C{i - 1})";

                sheet.Cells[i, 4] = "Общая сумма поставок";
                sheet.Cells[i, 5].Formulalocal = $"=СУММ(E3:E{i - 1})";
                sheet.Cells[i, 5].Font.Color = ColorTranslator.ToOle(System.Drawing.Color.Red);


                //закрашиваем названия столбцов
                Excel.Range mainRange = sheet.Range[sheet.Cells[2, 1], sheet.Cells[2, 5]];
                mainRange.Cells.Font.Color = ColorTranslator.ToOle(System.Drawing.Color.Red);

                //формируем границы
                Excel.Range range = (Excel.Range)sheet.Range[sheet.Cells[2, 1], sheet.Cells[i - 1, 5]];
                range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            }
            else
                MessageBox.Show("За данный период поставок не обнаружено");
        
        }
    }
}
