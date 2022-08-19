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
    /// Логика взаимодействия для EditSupplierWindow.xaml
    /// </summary>
    public partial class EditSupplierWindow : Window
    {
        public EditSupplierWindow()
        {
            InitializeComponent();
            var supp = Class.EF.Context.Suppliers.Where(i => i.IdSupplier == idSupplier).FirstOrDefault();
            txtName.Text = supp.NameSupplier;
            txtAddress.Text = supp.Address;
            txtPhone.Text = supp.Phone;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Введите наименование поставщика!");
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Введите адрес поставщика!");
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Введите телефон поставщика!");
                return;
            }
            if (txtPhone.Text.Any(Char.IsLetter))
            {
                MessageBox.Show("Проверьте телефон поставщика!");
                return;
            }
            var supp = Class.EF.Context.Suppliers.Where(i => i.IdSupplier == idSupplier).FirstOrDefault();
            supp.NameSupplier = txtName.Text;
            supp.Address = txtAddress.Text;
            supp.Phone = txtPhone.Text;
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
