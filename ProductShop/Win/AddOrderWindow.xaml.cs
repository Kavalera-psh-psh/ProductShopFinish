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
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public AddOrderWindow()
        {
            InitializeComponent();
            cmbEmployee.SelectedIndex = idEmp-1;
            cmbEmployee.DisplayMemberPath = "EmployeeFullName";
            cmbEmployee.ItemsSource = Class.EF.Context.Employees.ToList().Where(i => i.IdPosition==1);
            cmbCustomer.SelectedIndex = 0;
            cmbCustomer.DisplayMemberPath = "CustomerFullName";
            cmbCustomer.ItemsSource = Class.EF.Context.Customers.ToList();
            txbDateOrder.Text = Convert.ToString(DateTime.Now);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            var emp = Class.EF.Context.Employees.ToList().Where(i => i.EmployeeFullName.Contains(cmbEmployee.Text)).FirstOrDefault().IdEmployee;
            Class.EF.Context.Orders.Add(new Order
            {                
                IdEmployee = emp,
                IdCust = cmbCustomer.SelectedIndex+1,
                OrdDate = Convert.ToDateTime(DateTime.Today),
                DeliveryAddress = txbDeliveryAddress.Text
            });
            Class.EF.Context.SaveChanges();
            MessageBox.Show($"Заказ успешно добавлен!",
                                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

    }
}
