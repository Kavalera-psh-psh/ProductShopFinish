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
using ProductShop.EF;
using ProductShop.Win;
using ProductShop.Class;
using static ProductShop.Class.EF;
using static ProductShop.Class.IDClass;

namespace ProductShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Authorization authorization;
        public static Employee employee;
        public static Position position;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbLogin.Text))
            {
                MessageBox.Show("Введите логин!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txbPassword.Password))
            {
                MessageBox.Show("Введите пароль!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            authorization = Class.EF.Context.Authorizations.ToList().Where(i => i.Login == txbLogin.Text && i.Password == txbPassword.Password).FirstOrDefault();
            
            if (authorization == null)
            {
                MessageBox.Show("Пользователь не найден, повторите попытку", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            employee = Class.EF.Context.Employees.FirstOrDefault(i => i.IdEmployee == authorization.IdEmployee);
            position = Class.EF.Context.Positions.FirstOrDefault(i => i.IdPosition == employee.IdPosition);

            if (position.IdPosition == 1)
            {
                ManagerWindow manWin = new ManagerWindow();
                idEmp = employee.IdEmployee;
                manWin.Show();
                Application.Current.MainWindow.Close();
            }
            else if (position.IdPosition == 2)
            {
                StorekeeperWindow storeWin = new StorekeeperWindow();
                idEmp = employee.IdEmployee;
                storeWin.Show();
                Application.Current.MainWindow.Close();
            }

        }
    }
}
