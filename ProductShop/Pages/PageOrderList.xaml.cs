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

namespace ProductShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageOrderList.xaml
    /// </summary>
    public partial class PageOrderList : Page
    {
        int pageNum = 0;
        List<Order> list = new List<Order>();
        public PageOrderList()
        {
            InitializeComponent();
            LVOrder.ItemsSource = Class.EF.Context.Orders.ToList().OrderBy(i=>i.IdOrder);
            CBSort.SelectedIndex = 0;
            CBSort.ItemsSource = new List<string>
            {
                "По умолчанию",
                "Дата заказа (по возрастанию)",
                "Дата заказа (по убыванию)",
            };

        }

        public void Filtr()
        {
            // поиск
            list = Class.EF.Context.Orders.Where(i => i.Customer.LastName.Contains(tbFIOCustomer.Text) || i.Customer.FirstName.Contains(tbFIOCustomer.Text) ||
            i.Customer.Patronymic.Contains(tbFIOCustomer.Text))
                .Where(i => i.Employee.LastName.Contains(tbFIOEmployee.Text) || i.Employee.FirstName.Contains(tbFIOEmployee.Text) ||
                i.Employee.Patronymic.Contains(tbFIOEmployee.Text))
                .ToList();

            if (tbDateOrder.SelectedDate.HasValue)
            {
                list = list.Where(i => i.OrdDate == tbDateOrder.SelectedDate.Value).ToList();
            }

            // сортировка
            switch (CBSort.SelectedIndex)
            {
                case 1:
                    list = list.OrderBy(i => i.OrdDate).ToList();
                    break;
                case 2:
                    list = list.OrderByDescending(i => i.OrdDate).ToList();
                    break;
                default:
                    list = list.OrderBy(i => i.IdOrder).ToList();
                    break;
            }

            list = list.
                Skip(15 * pageNum).
                Take(15).
                ToList();

            LVOrder.ItemsSource = list;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            if (LVOrder.SelectedItem is Order order)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление заказа", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var result2 = MessageBox.Show("Точно?", "Удаление заказа", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result2 == MessageBoxResult.Yes)
                    {
                        Class.EF.Context.Orders.Remove(Class.EF.Context.Orders.Where(i => i.IdOrder == order.IdOrder).FirstOrDefault());
                        Class.EF.Context.SaveChanges();
                        MessageBox.Show("Запись успешно удалена!", "Удаление заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
                        LVOrder.ItemsSource = Class.EF.Context.Orders.ToList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись из списка!", "Удаление заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbFIOCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void tbFIOEmployee_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }
        private void CBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        private void BTNClear_Click(object sender, RoutedEventArgs e)
        {
            tbFIOCustomer.Text = "";
            tbFIOEmployee.Text = "";
            tbDateOrder.Text = "";
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
        private void BTNAddOrder_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow addOrder = new AddOrderWindow();
            addOrder.ShowDialog();
            Filtr();
        }

        private void btnProdInOrder_Click(object sender, RoutedEventArgs e)
        {
            if (LVOrder.SelectedItem is Order order)
            {
                idOrder = order.IdOrder;
                ProductInOrderWindow ProdOrd = new ProductInOrderWindow();
                ProdOrd.ShowDialog();
                LVOrder.ItemsSource = Class.EF.Context.Orders.ToList();

            }
            else
            {
                MessageBox.Show("Выберите заказ из списка!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbDateOrder_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }
    }
}
