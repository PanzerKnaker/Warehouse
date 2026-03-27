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
    /// Логика взаимодействия для SuplierWindow.xaml
    /// </summary>
    public partial class SuplierWindow : Window
    {
        string NameEmpl;
        string Surnamel;
        string Poste;
        string ind;
        //WarehouseEntities1 DB = new WarehouseEntities1();
        WarehouseEntities3 DB = new WarehouseEntities3();
        public SuplierWindow(string names, string surnames, string postes)
        {
            InitializeComponent();
            DGSuplier.ItemsSource = DB.Supliers.ToList();
            NameEmpl = names;
            Surnamel = surnames;
            Poste = postes;

        }


        private void BTBack_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu(NameEmpl, Surnamel, Poste);
            a.Show();
            this.Close();

        }



        private void DGSuplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Supliers item = DGSuplier.SelectedItem as Supliers;
            if (item == null) { return; }
            ind = $"{item.NameSup.ToString()}";
            SupNameEd.Text = item.NameSup;
            SupReferEd.Text = item.ReferTo;
            SupPostEd.Text = item.Post;
            SupAdresEd.Text = item.Adress;
            SupPhoneEd.Text = item.Phone;
            SupFaxEd.Text = item.Fax;
            SupEmailEd.Text = item.EMail;
        }

        private void BtSupAdd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNameSup.Text) || String.IsNullOrEmpty(TBReferSup.Text) || String.IsNullOrEmpty(TBPostSup.Text) || String.IsNullOrEmpty(TBNumSup.Text) || String.IsNullOrEmpty(TBFaxSup.Text) || String.IsNullOrEmpty(TBAdresSup.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Supliers sp = new Supliers
                {
                    NameSup = TBNameSup.Text,
                    ReferTo = TBReferSup.Text,
                    Post = TBPostSup.Text,
                    Adress = TBAdresSup.Text,
                    Phone = TBNumSup.Text,
                    Fax = TBFaxSup.Text,
                    EMail = TBEmailSup.Text
                };
                DB.Supliers.Add(sp);
                DB.SaveChanges();
                DGSuplier.ItemsSource = DB.Supliers.ToList();
                TBNameSup.Clear();
                TBReferSup.Clear();
                TBPostSup.Clear();
                TBAdresSup.Clear();
                TBNumSup.Clear();
                TBFaxSup.Clear();
                TBEmailSup.Clear();
            }
        }

        private void TbSupDel_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(SupNameEd.Text) || String.IsNullOrEmpty(SupReferEd.Text) || String.IsNullOrEmpty(SupPostEd.Text) || String.IsNullOrEmpty(SupAdresEd.Text) || String.IsNullOrEmpty(SupPhoneEd.Text) || String.IsNullOrEmpty(SupFaxEd.Text))
            {
                MessageBox.Show("Верни Деньги!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult res = MessageBox.Show($"Вы действительно хотите удалить данные {ind} ?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    Supliers item = DGSuplier.SelectedItem as Supliers;
                    DB.Supliers.Remove(item);
                    DGSuplier.ItemsSource = DB.Supliers.ToList();
                    SupNameEd.Clear();
                    SupReferEd.Clear();
                    SupPostEd.Clear();
                    SupAdresEd.Clear();
                    SupPhoneEd.Clear();
                    SupFaxEd.Clear();
                    SupEmailEd.Clear();
                    DB.SaveChanges();
                    DGSuplier.ItemsSource = DB.Supliers.ToList();
                }
            }
        }

        private void BTChangeSup_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(SupNameEd.Text) || String.IsNullOrEmpty(SupReferEd.Text) || String.IsNullOrEmpty(SupPostEd.Text) || String.IsNullOrEmpty(SupAdresEd.Text) || String.IsNullOrEmpty(SupPhoneEd.Text) || String.IsNullOrEmpty(SupFaxEd.Text))
            {
                MessageBox.Show("Верни Деньги Тварь!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Supliers item = DGSuplier.SelectedItem as Supliers;
                item.NameSup = SupNameEd.Text;
                item.ReferTo = SupReferEd.Text;
                item.Post = SupPostEd.Text;
                item.Adress = SupAdresEd.Text;
                item.Phone = SupPhoneEd.Text;
                item.Fax = SupFaxEd.Text;
                item.EMail = SupEmailEd.Text;
                MessageBox.Show("Изменения были внесены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DB.SaveChanges();
                DGSuplier.ItemsSource = DB.Supliers.ToList();

            }
        }
    }
}
