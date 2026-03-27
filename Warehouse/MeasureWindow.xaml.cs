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
    /// Логика взаимодействия для MeasureWindow.xaml
    /// </summary>
    public partial class MeasureWindow : Window
    {
        string ind;
        //WarehouseEntities1 DB = new WarehouseEntities1();
        WarehouseEntities3 DB = new WarehouseEntities3();
        public MeasureWindow()
        {
            InitializeComponent();
            DGMeasure.ItemsSource = DB.UnitToMeasure.ToList();
        }

        private void BTaddMesure_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNameMeasure.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UnitToMeasure un = new UnitToMeasure
                {
                    NameMeasure = TBNameMeasure.Text
                };
                DB.UnitToMeasure.Add(un);
                DB.SaveChanges();
                DGMeasure.ItemsSource = DB.UnitToMeasure.ToList();
                TBNameMeasure.Clear();
            }
        }

        private void BTChangeMesrue_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNameMeasure.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UnitToMeasure item = DGMeasure.SelectedItem as UnitToMeasure;
                item.NameMeasure = TBNameMeasure.Text;
                MessageBox.Show("Изменения были внесены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DB.SaveChanges();
                DGMeasure.ItemsSource = DB.UnitToMeasure.ToList();
            }
        }

        private void DGMeasure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UnitToMeasure item = DGMeasure.SelectedItem as UnitToMeasure;
            if (item == null) { return; }
            ind = $"{item.NameMeasure.ToString()}";
            TBNameMeasure.Text = item.NameMeasure;
        }

        private void BTDelMesure_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNameMeasure.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult res = MessageBox.Show($"Вы действительно хотите удалить данные {ind} ?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    UnitToMeasure item = DGMeasure.SelectedItem as UnitToMeasure;
                    DB.UnitToMeasure.Remove(item);
                    TBNameMeasure.Clear();
                    DB.SaveChanges();
                    DGMeasure.ItemsSource = DB.UnitToMeasure.ToList();
                }
            }
        }
    }
}
