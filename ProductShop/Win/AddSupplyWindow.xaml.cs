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
using static ProductShop.Class.EFSupply;
using static ProductShop.Class.IDClass;

namespace ProductShop.Win
{
    /// <summary>
    /// Логика взаимодействия для AddSupplyWindow.xaml
    /// </summary>
    public partial class AddSupplyWindow : Window
    {
        public AddSupplyWindow()
        {
            InitializeComponent();
            cmbSupplier.SelectedIndex = 0;
            cmbSupplier.DisplayMemberPath = "NameSupplier";
            cmbSupplier.ItemsSource = Class.EF.Context.Suppliers.ToList();
            cmbProduct.SelectedIndex = 0;
            cmbProduct.DisplayMemberPath = "NameProd";
            cmbProduct.ItemsSource = Class.EF.Context.Products.ToList();
            txbDateSupply.Text = Convert.ToString(DateTime.Now);
        }

        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            
            if (int.TryParse(txbCount.Text, out int number))
            {
                if (Convert.ToInt32(txbCount.Text) <= 0)
                {
                    MessageBox.Show("Количество товара должно быть больше 0!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                Class.EF.Context.Shipments.Add(new Shipment
                {
                    IdSupplier = cmbSupplier.SelectedIndex + 1,
                    IdProd = cmbProduct.SelectedIndex + 1,
                    DateShipment = Convert.ToDateTime(txbDateSupply.Text),
                    Quantity = Convert.ToInt32(txbCount.Text)
                });
                var addProduct = cmbProduct.SelectedItem as Product;
                if (addProduct != null)
                {
                    addProduct.InStock = addProduct.InStock + Convert.ToInt32(txbCount.Text);
                }
                Class.EF.Context.SaveChanges();
                MessageBox.Show("Поставка успешно добавлена!",
                                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Количество должно быть числовым значением!");
                return;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
