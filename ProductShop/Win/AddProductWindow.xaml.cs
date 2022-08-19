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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Введите наименование продукции!");
                return;
            }
            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("Введите цену продукции!");
                return;
            }
            if (decimal.TryParse(txtPrice.Text, out decimal number))
            {
                if (Convert.ToInt32(txtPrice.Text) <= 0)
                {
                    MessageBox.Show("Цена товара должна быть больше 0!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                Class.EF.Context.Products.Add(new Product
                {
                    NameProd = txtName.Text,
                    Description = txtDescription.Text,
                    InStock = 0,
                    Price = Convert.ToDecimal(txtPrice.Text)
                });
                Class.EF.Context.SaveChanges();
                MessageBox.Show($"Продукт успешно добавлен!",
                                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Цена должна быть числовым значением!");
                return;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
