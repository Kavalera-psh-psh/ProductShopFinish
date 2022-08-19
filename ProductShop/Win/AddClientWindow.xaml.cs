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
using ProductShop.EF;
using ProductShop.Win;
using ProductShop.Class;
using static ProductShop.Class.EF;
using static ProductShop.Class.IDClass;

namespace ProductShop.Win
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLName.Text))
            {
                MessageBox.Show("Введите фамилию клиента!");
                return;
            }
            if (string.IsNullOrEmpty(txFName.Text))
            {
                MessageBox.Show("Введите имя клиента!");
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Введите адрес клиента!");
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Введите телефон клиента!");
                return;
            }
            if (txtPhone.Text.Any(Char.IsLetter)) 
            {
                MessageBox.Show("Проверьте телефон клиента!");
                return;
            }
            Class.EF.Context.Customers.Add(new Customer
            {
                LastName = txtLName.Text,
                FirstName = txFName.Text,
                Patronymic = txtMName.Text,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
            });
            Class.EF.Context.SaveChanges();
            MessageBox.Show($"Клиент {txtLName.Text} {txFName.Text} успешно добавлен!",
                                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
