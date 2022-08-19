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
    /// Логика взаимодействия для AddProductInOrderWindow.xaml
    /// </summary>
    public partial class AddProductInOrderWindow : Window
    {
       
        public AddProductInOrderWindow()
        {
            InitializeComponent();
            cmbProduct.SelectedIndex = 0;
            cmbProduct.DisplayMemberPath = "NameProd";
            cmbProduct.ItemsSource = Class.EF.Context.Products.ToList();
            txbInStock.Text = Class.EF.Context.Products.Where(i => i.IdProd == cmbProduct.SelectedIndex + 1).FirstOrDefault().InStock.ToString();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void CheckCombo()
        {
            txbInStock.Text = Class.EF.Context.Products.Where(i => i.IdProd == cmbProduct.SelectedIndex + 1).FirstOrDefault().InStock.ToString();
        }

        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txbCount.Text, out int number))
            {
                if (Convert.ToInt32(txbCount.Text) <= 0)
                {
                    MessageBox.Show("Количество товара для заказа должно быть больше 0!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var qtyProd = Class.EF.Context.Products.Where(i => i.IdProd == cmbProduct.SelectedIndex + 1).FirstOrDefault().InStock;
                if (Convert.ToInt32(txbCount.Text) > qtyProd)
                {
                    MessageBox.Show("Недостаточное количество товара на складе!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    var checkProd = Class.EF.Context.OrdItems.Where(i => i.IdOrder == idOrder).ToList().Where(i => i.IdProd == (cmbProduct.SelectedItem as Product).IdProd).ToList();
                    var changeProd = Class.EF.Context.Products.ToList().Where(i => i.IdProd == (cmbProduct.SelectedItem as Product).IdProd).ToList();
                    if (checkProd.Count > 0)
                    {
                        checkProd.FirstOrDefault().Qty = checkProd.FirstOrDefault().Qty + Convert.ToInt32(txbCount.Text);
                        changeProd.FirstOrDefault().InStock = changeProd.FirstOrDefault().InStock - Convert.ToInt32(txbCount.Text);
                    }
                    else 
                    {
                        Class.EF.Context.OrdItems.Add(new OrdItem
                        {
                            IdOrder = idOrder,
                            IdProd = cmbProduct.SelectedIndex + 1,
                            Qty = Convert.ToInt32(txbCount.Text),

                        });
                        var addProduct = cmbProduct.SelectedItem as Product;
                        if (addProduct != null)
                        {
                            addProduct.InStock = addProduct.InStock - Convert.ToInt32(txbCount.Text);
                        }
                    }

                    Class.EF.Context.SaveChanges();
                    MessageBox.Show($"Продукт успешно добавлен!",
                                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                
                
            }
            else {
                MessageBox.Show("Количество должно быть числовым значением!");
                return;
            }
        }

        private void cmbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckCombo();
        }
    }
}
