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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        string NameEmpl;
        string Surnamel;
        string Poste;
        string ind;
        //WarehouseEntities1 DB = new WarehouseEntities1();
        WarehouseEntities3 DB = new WarehouseEntities3();
        public ClientWindow(string names, string surnames, string postes)
        {
            InitializeComponent();
            DGClient.ItemsSource = DB.Clients.ToList();
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

        private void BtCliAdd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBNameCli.Text) || String.IsNullOrEmpty(TBReferCli.Text) || String.IsNullOrEmpty(TBPostCli.Text) || String.IsNullOrEmpty(TBNumCli.Text) || String.IsNullOrEmpty(TBFaxCli.Text) || String.IsNullOrEmpty(TBAdresCli.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Clients sp = new Clients
                {
                    NameClient = TBNameCli.Text,
                    ReferToClient = TBReferCli.Text,
                    PostClient = TBPostCli.Text,
                    AdressClient = TBAdresCli.Text,
                    PhoneClient = TBNumCli.Text,
                    FaxClient = TBFaxCli.Text,
                    EmailClient = TBEmailCli.Text
                };
                DB.Clients.Add(sp);
                DB.SaveChanges();
                DGClient.ItemsSource = DB.Clients.ToList();
                TBNameCli.Clear();
                TBReferCli.Clear();
                TBPostCli.Clear();
                TBAdresCli.Clear();
                TBNumCli.Clear();
                TBFaxCli.Clear();
                TBEmailCli.Clear();
            }
        }

        private void BTChangeCli_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CliNameEd.Text) || String.IsNullOrEmpty(CliReferEd.Text) || String.IsNullOrEmpty(CliPostEd.Text) || String.IsNullOrEmpty(CliAdresEd.Text) || String.IsNullOrEmpty(CliPhoneEd.Text) || String.IsNullOrEmpty(CliFaxEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Clients item = DGClient.SelectedItem as Clients;
                item.NameClient = CliNameEd.Text;
                item.ReferToClient = CliReferEd.Text;
                item.PostClient = CliPostEd.Text;
                item.AdressClient = CliAdresEd.Text;
                item.PhoneClient = CliPhoneEd.Text;
                item.FaxClient = CliFaxEd.Text;
                item.EmailClient = CliEmailEd.Text;
                MessageBox.Show("Изменения были внесены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DB.SaveChanges();
                DGClient.ItemsSource = DB.Clients.ToList();

            }
        }

        private void TbCliDel_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CliNameEd.Text) || String.IsNullOrEmpty(CliReferEd.Text) || String.IsNullOrEmpty(CliPostEd.Text) || String.IsNullOrEmpty(CliAdresEd.Text) || String.IsNullOrEmpty(CliPhoneEd.Text) || String.IsNullOrEmpty(CliFaxEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult res = MessageBox.Show($"Вы действительно хотите удалить данные {ind} ?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    Clients item = DGClient.SelectedItem as Clients;
                    DB.Clients.Remove(item);
                    DGClient.ItemsSource = DB.Clients.ToList();
                    CliNameEd.Clear();
                    CliReferEd.Clear();
                    CliPostEd.Clear();
                    CliAdresEd.Clear();
                    CliPhoneEd.Clear();
                    CliFaxEd.Clear();
                    CliEmailEd.Clear();
                    DB.SaveChanges();
                    DGClient.ItemsSource = DB.Clients.ToList();
                }
            }
        }

        private void DGClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Clients item = DGClient.SelectedItem as Clients;
            if (item == null) { return; }
            ind = $"{item.NameClient.ToString()}";
            CliNameEd.Text = item.NameClient;
            CliReferEd.Text = item.ReferToClient;
            CliPostEd.Text = item.PostClient;
            CliAdresEd.Text = item.AdressClient;
            CliPhoneEd.Text = item.PhoneClient;
            CliFaxEd.Text = item.FaxClient;
            CliEmailEd.Text = item.EmailClient;
        }
    }
}
