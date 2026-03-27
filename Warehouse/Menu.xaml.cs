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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
   
    public partial class Menu : Window
    {
        string NameEmpl;
        string Surnamel;
        string Poste;
        WarehouseEntities3 DB = new WarehouseEntities3();
        public Menu(string names, string surnames, string postes)
        {
            InitializeComponent();
            NameEmpl = names;
            Surnamel = surnames;
            Poste = postes;
            LBName.Content =NameEmpl +" "+ Surnamel;
            LBPost.Content = Poste;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SuplierWindow a = new SuplierWindow(NameEmpl, Surnamel, Poste);
            a.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ClientWindow a = new ClientWindow(NameEmpl, Surnamel, Poste);
            a.Show();
            this.Hide();
        }

        private void BTExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BTEmp_Click(object sender, RoutedEventArgs e)
        {
            if (Poste == "Администратор")
            {
                EmpWindow a = new EmpWindow(NameEmpl, Surnamel, Poste);
                a.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("У вас нет прав чтобы открыть это окно", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void BTTovar_Click(object sender, RoutedEventArgs e)
        {
            TovarWindow a = new TovarWindow(NameEmpl, Surnamel, Poste);
            a.Show();
            this.Hide();
        }

        private void BTArrival_Click(object sender, RoutedEventArgs e)
        {
            ArrivalWindow a = new ArrivalWindow(NameEmpl, Surnamel, Poste);
            a.Show();
            this.Hide();
        }

        private void BTOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow a = new OrderWindow(NameEmpl, Surnamel, Poste);
            a.Show();
            this.Hide();

        }

        private void BTExitAcc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow a = new MainWindow();
            a.Show();
            this.Close();
        }
    }
}
