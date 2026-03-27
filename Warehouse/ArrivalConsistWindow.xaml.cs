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
    /// Логика взаимодействия для ArrivalConsistWindow.xaml
    /// </summary>
    public partial class ArrivalConsistWindow : Window
    {
        string ind;
        //WarehouseEntities1 DB = new WarehouseEntities1();
        WarehouseEntities3 DB = new WarehouseEntities3();
        public ArrivalConsistWindow()
        {
            InitializeComponent();
            DGArrivalCons.ItemsSource = DB.ConsistArrival.ToList();
            CBNumArr.ItemsSource = DB.Arrivals.Select(x => x.NumberArrival).ToList();
            CBNumTovar.ItemsSource = DB.BuildMaterials.Select(x => x.ItemNumber).ToList();
            CBNumArrEd.ItemsSource = DB.Arrivals.Select(x => x.NumberArrival).ToList();
            CBNumTovarEd.ItemsSource = DB.BuildMaterials.Select(x => x.ItemNumber).ToList();
        }

        private void BtAddConsArr_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CBNumArr.Text) || String.IsNullOrEmpty(CBNumTovar.Text) || String.IsNullOrEmpty(TBAmountArr.Text) || String.IsNullOrEmpty(TBVolumeArr.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ConsistArrival ca = new ConsistArrival
                {
                    NumberArrival = int.Parse(CBNumArr.Text),
                    ItemNumber = int.Parse(CBNumTovar.Text),
                    AmountArr = int.Parse(TBAmountArr.Text),
                    VolumeArr = int.Parse(TBVolumeArr.Text)
                };
                DB.ConsistArrival.Add(ca);
                DB.SaveChanges();
                DGArrivalCons.ItemsSource = DB.ConsistArrival.ToList();
                TBAmountArr.Clear();
                TBVolumeArr.Clear();
            }
        }

        private void BTChangeConsArr_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CBNumArrEd.Text) || String.IsNullOrEmpty(CBNumTovarEd.Text) || String.IsNullOrEmpty(TBAmountArrEd.Text) || String.IsNullOrEmpty(TBVolumeArrEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ConsistArrival item = DGArrivalCons.ItemsSource as ConsistArrival;
                item.NumberArrival = int.Parse(CBNumArrEd.Text);
                item.ItemNumber = int.Parse(CBNumTovarEd.Text);
                item.AmountArr = int.Parse(TBAmountArrEd.Text);
                item.VolumeArr = int.Parse(TBVolumeArrEd.Text);
                MessageBox.Show("Изменения были внесены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DB.SaveChanges();
                DGArrivalCons.ItemsSource = DB.ConsistArrival.ToList();
            }
        }
        private void DGArrivalCons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConsistArrival item = new ConsistArrival();
            if (item == null) { return; }
            ind = $"{item.NumberArrival.ToString()}";
            CBNumArrEd.Text = Convert.ToString(item.NumberArrival);
            CBNumTovarEd.Text = Convert.ToString(item.ItemNumber);
            TBAmountArrEd.Text = Convert.ToString(TBAmountArr);
            TBVolumeArrEd.Text = Convert.ToString(TBVolumeArrEd);

        }

        private void TBDelConsArr_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CBNumArrEd.Text) || String.IsNullOrEmpty(CBNumTovarEd.Text) || String.IsNullOrEmpty(TBAmountArrEd.Text) || String.IsNullOrEmpty(TBVolumeArrEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult res = MessageBox.Show($"Вы действительно хотите удалить данные {ind} ?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    ConsistArrival item = DGArrivalCons.ItemsSource as ConsistArrival;
                    DB.ConsistArrival.Remove(item);
                    DB.SaveChanges();
                    DGArrivalCons.ItemsSource = DB.ConsistArrival.ToList();
                    TBAmountArrEd.Clear();
                    TBVolumeArrEd.Clear();
                }
            }
        }
    }
}
