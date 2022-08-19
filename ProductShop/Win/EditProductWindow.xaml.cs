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
using static ProductShop.Class.IDClass;
using static ProductShop.Class.EFStorekeeper;

namespace ProductShop.Win
{
    /// <summary>
    /// Логика взаимодействия для EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {

        EF.Product editProduct;
        public EditProductWindow()
        {
            InitializeComponent();
            var prod = upProd.Products.ToList().Where(i => i.IdProd == idProduct).FirstOrDefault();
            txtName.Text = prod.NameProd;
            txtDescription.Text = prod.Description;
            txtPrice.Text = prod.Price.ToString();
        }

        public EditProductWindow(EF.Product product)
        {
            InitializeComponent();            
            txtName.Text = product.NameProd;
            txtDescription.Text = product.Description;
            txtPrice.Text = product.Price.ToString();
            editProduct = product;

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
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
                if (Convert.ToDecimal(txtPrice.Text) <= 0)
                {
                    MessageBox.Show("Цена товара должна быть больше 0!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                editProduct.NameProd = txtName.Text;
                editProduct.Description = txtDescription.Text;
                editProduct.Price = Convert.ToDecimal(txtPrice.Text);
                Class.EF.Context.SaveChanges();
                MessageBox.Show($"Продукт успешно изменен!",
                                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Цена должна быть числовым значением!");
                return;
            }
        }
    }
}
