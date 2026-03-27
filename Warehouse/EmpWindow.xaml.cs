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
    /// Логика взаимодействия для EmpWindow.xaml
    /// </summary>
    public partial class EmpWindow : Window
    {
        string NameEmpl;
        string Surnamel;
        string Poste;
        string ind;
        //WarehouseEntities1 DB = new WarehouseEntities1();
        WarehouseEntities3 DB = new WarehouseEntities3();
        public EmpWindow(string names, string surnames, string postes)
        {
            InitializeComponent();
            DGEmp.ItemsSource = DB.Employees.ToList();
            NameEmpl = names;
            Surnamel = surnames;
            Poste = postes;
        }


        private void DGEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employees item = DGEmp.SelectedItem as Employees;
            if (item == null) { return; }
            ind = $"{item.SurnameEmp.ToString()}, {item.NameEmp.ToString()}";
            TBSurnameEdEmp.Text = item.SurnameEmp;
            TBNameEmpEd.Text = item.NameEmp;
            TBLastnameEmpEd.Text = item.Lastname;
            TBPostEmpEd.Text = item.PostEmp;
            DPBirthDateEd.SelectedDate = item.BirthDate;
            TBAdressEmpEd.Text = item.AdressEmp;
            TBPhoneEmpEd.Text = item.PhoneEmp;
            TBEmailEmpEd.Text = item.EmailEmp;
            TBLoginEmpEd.Text = item.Login;
            TBPassEmpEd.Text = item.Password;
        }

        private void BTChangeEmp_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBSurnameEdEmp.Text) || String.IsNullOrEmpty(TBNameEmpEd.Text) || String.IsNullOrEmpty(TBPostEmpEd.Text) || String.IsNullOrEmpty(TBAdressEmpEd.Text) || String.IsNullOrEmpty(TBPhoneEmpEd.Text) || String.IsNullOrEmpty(TBLoginEmpEd.Text) || String.IsNullOrEmpty(TBPassEmpEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Employees item = DGEmp.SelectedItem as Employees;
                item.SurnameEmp = TBSurnameEdEmp.Text;
                item.NameEmp = TBNameEmpEd.Text;
                item.Lastname = TBLastnameEmpEd.Text;
                item.PostEmp = TBPostEmpEd.Text;
                item.BirthDate = Convert.ToDateTime(DPBirthDateEd.SelectedDate);
                item.AdressEmp = TBAdressEmpEd.Text;
                item.PhoneEmp = TBPhoneEmpEd.Text;
                item.EmailEmp = TBEmailEmpEd.Text;
                item.Login = TBLoginEmpEd.Text;
                item.Password = TBPassEmpEd.Text;
                MessageBox.Show("Изменения были внесены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DB.SaveChanges();
                DGEmp.ItemsSource = DB.Employees.ToList();
            }
        }

        private void TbEmpDel_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBSurnameEdEmp.Text) || String.IsNullOrEmpty(TBNameEmpEd.Text) || String.IsNullOrEmpty(TBPostEmpEd.Text) || String.IsNullOrEmpty(TBAdressEmpEd.Text) || String.IsNullOrEmpty(TBPhoneEmpEd.Text) || String.IsNullOrEmpty(TBLoginEmpEd.Text) || String.IsNullOrEmpty(TBPassEmpEd.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult res = MessageBox.Show($"Вы действительно хотите удалить данные {ind} ?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    Employees item = DGEmp.SelectedItem as Employees;
                    DB.Employees.Remove(item);
                    DGEmp.ItemsSource = DB.Employees.ToList();
                    TBSurnameEdEmp.Clear();
                    TBNameEmpEd.Clear();
                    TBLastnameEmpEd.Clear();
                    TBPostEmpEd.Clear();
                    TBAdressEmpEd.Clear();
                    TBPhoneEmpEd.Clear();
                    TBEmailEmpEd.Clear();
                    TBLoginEmpEd.Clear();
                    TBPassEmpEd.Clear();
                    DB.SaveChanges();
                    DGEmp.ItemsSource = DB.Employees.ToList();
                }
            }
        }

        private void BTBackEmp_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu(NameEmpl, Surnamel, Poste);
            a.Show();
            this.Close();
        }

        private void BtEmpAdd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBSurnameEmp.Text) || String.IsNullOrEmpty(TBNameEmp.Text) || String.IsNullOrEmpty(TBPostEmp.Text) || String.IsNullOrEmpty(TBAdressEmp.Text) || String.IsNullOrEmpty(TBPhoneEmp.Text) || String.IsNullOrEmpty(TBLoginEmp.Text) || String.IsNullOrEmpty(TBPassEmp.Text))
            {
                MessageBox.Show("Данные не введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Employees em = new Employees
                {
                    SurnameEmp = TBSurnameEmp.Text,
                    NameEmp = TBNameEmp.Text,
                    Lastname = TBLastnameEmp.Text,
                    PostEmp = TBPostEmp.Text,
                    BirthDate = Convert.ToDateTime(DPBirthEmp.SelectedDate),
                    AdressEmp = TBAdressEmp.Text,
                    PhoneEmp = TBPhoneEmp.Text,
                    EmailEmp = TBEmailEmp.Text,
                    Login = TBLoginEmp.Text,
                    Password = TBPassEmp.Text
                };
                DB.Employees.Add(em);
                DB.SaveChanges();
                DGEmp.ItemsSource = DB.Employees.ToList();
                TBSurnameEmp.Clear();
                TBNameEmp.Clear();
                TBLastnameEmp.Clear();
                TBPostEmp.Clear();
                TBAdressEmp.Clear();
                TBPhoneEmp.Clear();
                TBEmailEmp.Clear();
                TBLoginEmp.Clear();
                TBPassEmp.Clear();

            }
        }
    }
}
