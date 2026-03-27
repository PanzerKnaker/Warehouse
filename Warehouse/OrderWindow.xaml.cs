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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace Warehouse2
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : System.Windows.Window
    {
        //dynamic excel = Microsoft.VisualBasic.Interaction.CreateObject("Excel.Application", string.Empty);
        string ind;
        string NameEmpl;
        string Surnamel;
        string Poste;
        //WarehouseEntities1 DB = new WarehouseEntities1();
        WarehouseEntities3 DB = new WarehouseEntities3();
        public OrderWindow(string names, string surnames, string postes)
        {
            InitializeComponent();
            DGOrder.ItemsSource = DB.Orders.ToList();
            CBCliOrd.ItemsSource = DB.Clients.ToList();
            CBCliOrdEd.ItemsSource = DB.Clients.ToList();
            CBEmpOrd.ItemsSource = DB.Employees.ToList();
            CBEmpOrdEd.ItemsSource = DB.Employees.ToList();
            NameEmpl = names;
            Surnamel = surnames;
            Poste = postes;
        }

        private void BTOrdered_Click(object sender, RoutedEventArgs e)
        {
            OrderedWindow a = new OrderedWindow();
            a.ShowDialog();
        }

        private void BTBackOrd_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu( NameEmpl, Surnamel, Poste);
            a.Show();
            this.Close();
        }

        private void DGOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Orders item = DGOrder.ItemsSource as Orders;
            if (item == null) { return; }
            ind = $"{item.IDOrder.ToString()}";
            TBNumOrdEd.Text = Convert.ToString(item.IDOrder);
            CBCliOrdEd.SelectedItem = item;
            CBEmpOrdEd.SelectedItem = item;
            DPDateOrdEd.SelectedDate = Convert.ToDateTime(item.ShippingDate);
            TBNameOrdEd.Text = item.RecipientName;
            TBAdressOrdEd.Text = item.RecipientAdress;
        }

        private void BtAddOrd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNumOrd.Text) || String.IsNullOrEmpty(CBCliOrd.Text) || String.IsNullOrEmpty(CBEmpOrd.Text) || String.IsNullOrEmpty(TBNameOrd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Orders or = new Orders
                {
                    IDOrder = int.Parse(TBNumOrd.Text),
                    IDClient = (CBCliOrd.SelectedItem as Clients).IDClient,
                    IDEmloyee = (CBEmpOrd.SelectedItem as Employees).IDEmployee,
                    ShippingDate = Convert.ToDateTime(DPDateOrd.SelectedDate),
                    RecipientName = TBNameOrd.Text,
                    RecipientAdress = TBAdressOrd.Text
                };
                DB.Orders.Add(or);
                DB.SaveChanges();
                DGOrder.ItemsSource = DB.Orders.ToList();
                TBNumOrd.Clear();
                TBNameOrd.Clear();
                TBAdressOrd.Clear();
                
            }
        }

        private void BTChangeArr_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNumOrdEd.Text) || String.IsNullOrEmpty(CBCliOrdEd.Text) || String.IsNullOrEmpty(CBEmpOrdEd.Text) || String.IsNullOrEmpty(TBNameOrdEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Orders item = DGOrder.SelectedItem as Orders;
                item.IDOrder = int.Parse(TBNumOrdEd.Text);
                item.IDClient = (CBCliOrdEd.SelectedItem as Clients).IDClient;
                item.IDEmloyee = (CBEmpOrdEd.SelectedItem as Employees).IDEmployee;
                item.ShippingDate = Convert.ToDateTime(DPDateOrdEd.SelectedDate);
                item.RecipientName = TBNameOrdEd.Text;
                item.RecipientAdress = TBAdressOrdEd.Text;
                MessageBox.Show("Изменения были внесены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DB.SaveChanges();
                DGOrder.ItemsSource = DB.Orders.ToList();
            }
        }


        private void TBDelArr_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNumOrdEd.Text) || String.IsNullOrEmpty(CBCliOrdEd.Text) || String.IsNullOrEmpty(CBEmpOrdEd.Text) || String.IsNullOrEmpty(TBNameOrdEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult res = MessageBox.Show($"Вы действительно хотите удалить данные {ind} ?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    Orders item = DGOrder.SelectedItem as Orders;
                    DB.Orders.Remove(item);
                    DB.SaveChanges();
                    DGOrder.ItemsSource = DB.Orders.ToList();
                    TBNumOrdEd.Clear();
                    TBNameOrdEd.Clear();
                    TBAdressOrdEd.Clear();
                }
            }
        }

        private void BtЗPechOrd_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < DGOrder.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = DGOrder.Columns[j].Header;
            }
            for (int i = 0; i < DGOrder.Columns.Count; i++)
            {
                for (int j = 0; j < DGOrder.Items.Count; j++)
                {
                    TextBlock b = DGOrder.Columns[i].GetCellContent(DGOrder.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }
        
        /*public static class ExportManager
        {
            public static void ExportToCSV(System.Windows.Controls.DataGrid dg) // dg-наш подопытный
            {
                if (dg.Items.Count == 0)
                    return;

                dg.SelectAllCells();// выбираем все ячейки
                dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;// подготавливаем clipboard к копированию грида с заголовком
                ApplicationCommands.Copy.Execute(null, dg);// "copy"
                dg.UnselectAllCells();
                var stream = (System.IO.Stream)Clipboard.GetData(DataFormats.CommaSeparatedValue);// взятие данных из clipboard
                var encoding = System.Text.Encoding.GetEncoding("UTF-8");
                var reader = new System.IO.StreamReader(stream, encoding);
                string result = reader.ReadToEnd();// копирование их в строку в UTF-8 кодировке
                Clipboard.Clear();

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = "c:\\";
                ofd.Filter = "CSV (*.csv)|*.csv|All files (*.*)|*.*";
                ofd.RestoreDirectory = true;
                ofd.CheckFileExists = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(ofd.FileName))
                    {
                        file.WriteLine(result);
                    }
                }
                
            }
            
        }
        */

    }
}
