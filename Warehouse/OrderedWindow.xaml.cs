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
    /// Логика взаимодействия для OrderedWindow.xaml
    /// </summary>
    public partial class OrderedWindow : Window
    {
        string ind;
        //WarehouseEntities1 DB = new WarehouseEntities1();
        WarehouseEntities3 DB = new WarehouseEntities3();
        public OrderedWindow()
        {
            InitializeComponent();
            DGOrderCons.ItemsSource = DB.Ordered.ToList();
            CBNumOrd.ItemsSource = DB.Orders.Select(x => x.IDOrder).ToList();
            CBNumTovar.ItemsSource = DB.BuildMaterials.Select(x => x.ItemNumber).ToList();
        }

        private void BtAddConsOrd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CBNumOrd.Text) || String.IsNullOrEmpty(CBNumTovar.Text) || String.IsNullOrEmpty(TBAmountOrd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Ordered co = new Ordered
                {
                    IDOrder = int.Parse(CBNumOrd.Text),
                    ItemNumber = int.Parse(CBNumTovar.Text),
                    AmountOrd = int.Parse(TBAmountOrd.Text)
                };
                DB.Ordered.Add(co);
                DB.SaveChanges();
                DGOrderCons.ItemsSource = DB.Ordered.ToList();
                TBAmountOrd.Clear();
            }
        }

        private void DGOrderCons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ordered item = new Ordered();
            if (item == null) { return; }
            ind = $"{item.IDOrder.ToString()}";
            CBNumOrdEd.Text = Convert.ToString(item.IDOrder);
            CBNumTovarEd.Text = Convert.ToString(item.ItemNumber);
            TBAmountOrdEd.Text = Convert.ToString(item.AmountOrd);

        }

        private void BTChangeConsOrd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CBNumOrdEd.Text) || String.IsNullOrEmpty(CBNumTovarEd.Text) || String.IsNullOrEmpty(TBAmountOrdEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Ordered item = DGOrderCons.SelectedItem as Ordered;
                item.IDOrder = int.Parse(CBNumOrdEd.Text);
                item.ItemNumber = int.Parse(CBNumTovarEd.Text);
                item.AmountOrd = int.Parse(TBAmountOrdEd.Text);
                MessageBox.Show("Изменения были внесены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DB.SaveChanges();
                DGOrderCons.ItemsSource = DB.Ordered.ToList();
            }
        }

        private void TBDelConsOrd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CBNumOrdEd.Text) || String.IsNullOrEmpty(CBNumTovarEd.Text) || String.IsNullOrEmpty(TBAmountOrdEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult res = MessageBox.Show($"Вы действительно хотите удалить данные {ind} ?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    Ordered item = DGOrderCons.SelectedItem as Ordered;
                    DB.Ordered.Remove(item);
                    DB.SaveChanges();
                    DGOrderCons.ItemsSource = DB.Ordered.ToList();
                    TBAmountOrdEd.Clear();
                }
            }
        }
    }
}
