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
    /// Логика взаимодействия для ProductInOrderWindow.xaml
    /// </summary>
    public partial class ProductInOrderWindow : Window
    {
        int pageNum = 0;
        List<CostOrder> list = new List<CostOrder>();
        
        public ProductInOrderWindow()
        {
            InitializeComponent();
            lvProductInOrder.ItemsSource = Class.EF.Context.CostOrders.ToList().Where(i=>i.IdOrder == idOrder);
            CBSort.SelectedIndex = 0;
            CBSort.ItemsSource = new List<string>
            { 
                "По умолчанию",
                "Наименование, А-Я",
                "Наименование, Я-А",
                "Цена (по возрастанию)",
                "Цена (по убыванию)",
            };
            txbFinal.Text = Convert.ToString(Class.EF.Context.CostOrders.Where(i => i.IdOrder == idOrder).Sum(i=>i.Price));
        }

        public void Filtr()
        {
            // поиск
            list = Class.EF.Context.CostOrders.Where(i => i.IdOrder == idOrder).Where(i => i.NameProd.Contains(txbNameProd.Text)).ToList();

            // сортировка
            switch (CBSort.SelectedIndex)
            {
                case 1:
                    list = list.OrderBy(i => i.NameProd).ToList();
                    break;
                case 2:
                    list = list.OrderByDescending(i => i.NameProd).ToList();
                    break;
                case 3:
                    list = list.OrderBy(i => i.Price).ToList();
                    break;
                case 4:
                    list = list.OrderByDescending(i => i.Price).ToList();
                    break;
                default:
                    list = list.OrderBy(i => i.IdOrder).ToList();
                    break;
            }

            list = list.
                Skip(15 * pageNum).
                Take(15).
                ToList();

            lvProductInOrder.ItemsSource = list;
            txbFinal.Text = Convert.ToString(Class.EF.Context.CostOrders.Where(i => i.IdOrder == idOrder).Sum(i => i.Price));

        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (lvProductInOrder.SelectedItem is CostOrder costOrder)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить продукт?", "Удаление продукта", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var result2 = MessageBox.Show("Точно?", "Удаление продукта", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result2 == MessageBoxResult.Yes)
                    {
                        var deleteProduct = Class.EF.Context.OrdItems.Where(i => i.IdOrder == costOrder.IdOrder && i.Product.NameProd == costOrder.NameProd).FirstOrDefault();
                        Class.EF.Context.OrdItems.Remove(Class.EF.Context.OrdItems.Where(i => i.IdOrder == costOrder.IdOrder && i.Product.NameProd == costOrder.NameProd).FirstOrDefault());
                        
                        if (deleteProduct != null)
                        {
                            var prod = Class.EF.Context.Products.Where(i => i.IdProd == deleteProduct.IdProd).FirstOrDefault();
                            prod.InStock = prod.InStock + deleteProduct.Qty;
                        }
                        Class.EF.Context.SaveChanges();
                        MessageBox.Show("Продукт успешно удален!", "Удаление продукта", MessageBoxButton.OK, MessageBoxImage.Warning);
                        lvProductInOrder.ItemsSource = Class.EF.Context.CostOrders.Where(i => i.IdOrder == idOrder).ToList();
                        txbFinal.Text = Convert.ToString(Class.EF.Context.CostOrders.Where(i => i.IdOrder == idOrder).Sum(i => i.Price));
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись из списка!", "Удаление заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void txbNAmeProd_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void CBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        private void BTNClear_Click(object sender, RoutedEventArgs e)
        {
            txbNameProd.Text = "";
            CBSort.SelectedIndex = 0;
        }

        private void btmBackPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageNum > 0)
            {
                pageNum--;
                tblPage.Text = (pageNum + 1).ToString();
            }
            Filtr();
        }

        private void btmNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count > 0)
            {
                pageNum++;
                tblPage.Text = (pageNum + 1).ToString();
            }
            Filtr();
        }
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProductInOrderWindow addProd = new AddProductInOrderWindow();
            addProd.ShowDialog();
            Filtr();
        }
    }
}
