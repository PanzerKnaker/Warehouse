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
    /// Логика взаимодействия для TovarWindow.xaml
    /// </summary>
    public partial class TovarWindow : Window
    {
        string NameEmpl;
        string Surnamel;
        string Poste;
        string ind;
        //WarehouseEntities1 DB = new WarehouseEntities1();
        WarehouseEntities3 DB = new WarehouseEntities3();
        public TovarWindow(string names, string surnames, string postes)
        {
            InitializeComponent();
            DGTovar.ItemsSource = DB.BuildMaterials.ToList();
            CBMeasure.ItemsSource = DB.UnitToMeasure.Select(x => x.NameMeasure).ToList();
            CBMeasureEd.ItemsSource = DB.UnitToMeasure.Select(x => x.NameMeasure).ToList();
            NameEmpl = names;
            Surnamel = surnames;
            Poste = postes;
        }

        private void BTBackTov_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu(NameEmpl, Surnamel, Poste);
            a.Show();
            this.Close();
        }

        private void BtAddTov_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNumTov.Text) || String.IsNullOrEmpty(TBNameTov.Text) || String.IsNullOrEmpty(CBMeasure.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                BuildMaterials tv = new BuildMaterials
                {
                    ItemNumber = int.Parse(TBNumTov.Text),
                    Mark = TBMarkTov.Text,
                    NameItem = TBNameTov.Text,
                    IDMeasure = int.Parse(CBMeasure.Text),
                    OnWarehouse = Convert.ToBoolean(ChekTovar.IsChecked),
                };
                DB.BuildMaterials.Add(tv);
                DB.SaveChanges();
                DGTovar.ItemsSource = DB.BuildMaterials.ToList();
                TBNumTov.Clear();
                TBMarkTov.Clear();
                TBNameTov.Clear();
            }
        }
        private void BTMesure_Click(object sender, RoutedEventArgs e)
        {
            MeasureWindow a = new MeasureWindow();
            a.ShowDialog();
        }

        private void BTChangeTov_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNumTovEd.Text) || String.IsNullOrEmpty(TBNameTovEd.Text) || String.IsNullOrEmpty(CBMeasureEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                BuildMaterials item = DGTovar.SelectedItem as BuildMaterials;
                item.ItemNumber = int.Parse(TBNumTovEd.Text);
                item.Mark = TBMarkTovEd.Text;
                item.NameItem = TBNameTovEd.Text;
                item.IDMeasure = int.Parse(CBMeasureEd.Text);
                item.OnWarehouse = Convert.ToBoolean(ChekTovarEd.IsChecked);
                MessageBox.Show("Изменения были внесены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DB.SaveChanges();
                DGTovar.ItemsSource = DB.BuildMaterials.ToList();
            }
        }


        private void DGTovar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BuildMaterials item = DGTovar.SelectedItem as BuildMaterials;
            if (item == null) { return; }
            ind = $"{item.ItemNumber.ToString()}, {item.NameItem.ToString()}";
            TBNumTovEd.Text = Convert.ToString(item.ItemNumber);
            TBMarkTovEd.Text = item.Mark;
            TBNameTovEd.Text = item.NameItem;
            CBMeasureEd.Text = Convert.ToString(item.IDMeasure);
        }

        private void TBDelTov_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNumTovEd.Text) || String.IsNullOrEmpty(TBNameTovEd.Text) || String.IsNullOrEmpty(CBMeasureEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult res = MessageBox.Show($"Вы действительно хотите удалить данные {ind} ?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    BuildMaterials item = DGTovar.SelectedItem as BuildMaterials;
                    DB.BuildMaterials.Remove(item);
                    TBNumTovEd.Clear();
                    TBMarkTovEd.Clear();
                    TBNameTovEd.Clear();
                    DB.SaveChanges();
                    DGTovar.ItemsSource = DB.BuildMaterials.ToList();
                }
            }
        }
    }
}