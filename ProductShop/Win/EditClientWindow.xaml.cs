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
    /// Логика взаимодействия для EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        public EditClientWindow()
        {
            InitializeComponent();
            var cust = Class.EF.Context.Customers.Where(i => i.IdCust == idCustomer).FirstOrDefault();
            txtLName.Text = cust.LastName;
            txtFName.Text = cust.FirstName;
            txtMName.Text = cust.Patronymic;
            txtAddress.Text = cust.Address;
            txtPhone.Text = cust.Phone;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLName.Text))
            {
                MessageBox.Show("Введите фамилию клиента!");
                return;
            }
            if (string.IsNullOrEmpty(txtFName.Text))
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
            var cust = Class.EF.Context.Customers.Where(i => i.IdCust == idCustomer).FirstOrDefault();
            cust.LastName = txtLName.Text;
            cust.FirstName = txtFName.Text;
            cust.Patronymic = txtMName.Text;
            cust.Address = txtAddress.Text;
            cust.Phone = txtPhone.Text;
            Class.EF.Context.SaveChanges();
            MessageBox.Show("Данные успешно сохранены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
