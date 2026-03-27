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

namespace Warehouse2
{
    /// <summary>
    /// Логика взаимодействия для ArrivalWindow.xaml
    /// </summary>
    public partial class ArrivalWindow : Window
    {
        string NameEmpl;
        string Surnamel;
        string Poste;
        string ind;
        //WarehouseEntities1 DB = new WarehouseEntities1();
        WarehouseEntities3 DB = new WarehouseEntities3();
        public ArrivalWindow(string names, string surnames, string postes)
        {
            InitializeComponent();
            DGArrival.ItemsSource = DB.Arrivals.ToList();
            CBAcceptArr.ItemsSource = DB.Employees.Select(x => x.SurnameEmp).ToList();
            CBSuplierArr.ItemsSource = DB.Supliers.Select(x => x.NameSup).ToList();
            CBAcceptArrEd.ItemsSource = DB.Supliers.Select(x => x.NameSup).ToList();
            CBSuplierEd.ItemsSource = DB.Employees.Select(x => x.SurnameEmp).ToList();
            NameEmpl = names;
            Surnamel = surnames;
            Poste = postes;
        }

        private void BTBackArr_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu(NameEmpl, Surnamel, Poste);
            a.Show();
            this.Close();
        }

        private void BTConsist_Click(object sender, RoutedEventArgs e)
        {
            ArrivalConsistWindow a = new ArrivalConsistWindow();
            a.ShowDialog();

        }

        private void DGArrival_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Arrivals item = new Arrivals();
            if (item == null) { return; }
            ind = $"{item.NumberArrival.ToString()}";
            TBNumArrEd.Text = Convert.ToString(item.NumberArrival);
            CBAcceptArrEd.Text = Convert.ToString(item.ArrivalAccepted);
            CBSuplierEd.Text = Convert.ToString(item.Suplier);
            DPDateArrEd.SelectedDate = item.ArrivalDate;
        }

        private void BtAddArr_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNumArr.Text) || String.IsNullOrEmpty(CBAcceptArr.Text) || String.IsNullOrEmpty(CBSuplierArr.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Arrivals ar = new Arrivals
                {
                    NumberArrival = int.Parse(TBNumArr.Text),
                    ArrivalAccepted = int.Parse(CBAcceptArr.Text),
                    Suplier = int.Parse(CBSuplierArr.Text),
                    ArrivalDate = Convert.ToDateTime(DPDateArr.SelectedDate)
                };
                DB.Arrivals.Add(ar);
                DB.SaveChanges();
                DGArrival.ItemsSource = DB.Arrivals.ToList();
                TBNumArr.Clear();

            }
        }

        private void BTChangeArr_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNumArrEd.Text) || String.IsNullOrEmpty(CBAcceptArrEd.Text) || String.IsNullOrEmpty(CBSuplierEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Arrivals item = DGArrival.SelectedItem as Arrivals;
                item.NumberArrival = int.Parse(TBNumArrEd.Text);
                item.ArrivalAccepted = int.Parse(CBAcceptArrEd.Text);
                item.Suplier = int.Parse(CBSuplierEd.Text);
                item.ArrivalDate = Convert.ToDateTime(DPDateArrEd.Text);
                MessageBox.Show("Изменения были внесены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DB.SaveChanges();
                DGArrival.ItemsSource = DB.Arrivals.ToList();
            }
            
        }

        private void BTDelArr_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNumArrEd.Text) || String.IsNullOrEmpty(CBAcceptArrEd.Text) || String.IsNullOrEmpty(CBSuplierEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult res = MessageBox.Show($"Вы действительно хотите удалить данные {ind} ?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    Arrivals item = DGArrival.SelectedItem as Arrivals;
                    DB.Arrivals.Remove(item);
                    TBNumArrEd.Clear();
                    DB.SaveChanges();
                    DGArrival.ItemsSource = DB.Arrivals.ToList();
                }
            }
        }
    }
}
