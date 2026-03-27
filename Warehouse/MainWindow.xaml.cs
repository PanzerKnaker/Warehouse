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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Warehouse2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        //WarehouseEntities1 db = new WarehouseEntities1();
        WarehouseEntities3 db = new WarehouseEntities3();
        public MainWindow()
        {
            InitializeComponent();
        }


        private void BtExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtVhod_Click(object sender, RoutedEventArgs e)
        {

                       if (String.IsNullOrEmpty(TBLogin.Text) || String.IsNullOrEmpty(TBPass.Password))
                       {
                           MessageBox.Show("Заполните все поля", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                           if (String.IsNullOrEmpty(TBLogin.Text))
                           {
                               TBLogin.BorderBrush = Brushes.Red;
                           }
                           if (String.IsNullOrEmpty(TBPass.Password))
                           {
                               TBPass.BorderBrush = Brushes.Red;
                           }
                           return;
                       }
                       Employees auto = db.Employees.Where(h => h.Login == TBLogin.Text && h.Password == TBPass.Password).FirstOrDefault();
                       if (auto == null)
                       {
                           MessageBox.Show("Неправильный логин или пароль!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                       }
                       else
                       {
                            if (auto.PostEmp == "Администратор")
                            {
                            Menu a = new Menu(auto.NameEmp, auto.SurnameEmp, auto.PostEmp);
                            a.Show();
                            this.Hide();
                            }
                            else
                            {
                            Menu a = new Menu(auto.NameEmp, auto.SurnameEmp, auto.PostEmp);
                            a.Show();
                            this.Hide();

                            }
                           
                       } 
        }

    }
}
