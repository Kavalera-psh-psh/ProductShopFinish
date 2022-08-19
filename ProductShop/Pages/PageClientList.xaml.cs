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
    /// Логика взаимодействия для PageClientList.xaml
    /// </summary>
    public partial class PageClientList : Page
    {
        int pageNum = 0;
        List<Customer> list = new List<Customer>();
        public PageClientList()
        {
            InitializeComponent();
            LVClient.ItemsSource = Class.EF.Context.Customers.ToList().OrderBy(i => i.IdCust);

            CBSort.SelectedIndex = 0;
            CBSort.ItemsSource = new List<string>
            {
                "По умолчанию",
                "ФИО, А-Я",
                "ФИО, Я-А",
            };
        }
        public void Filtr()
        {
            // поиск
            list = Class.EF.Context.Customers.Where(i => i.FirstName.Contains(TBFIO.Text) || i.LastName.Contains(TBFIO.Text) || i.Patronymic.Contains(TBFIO.Text))
                .Where(i => i.Address.Contains(TBAddress.Text))
                .Where(i => i.Phone.Contains(TBPhone.Text))
                .ToList();

            // сортировка
            switch (CBSort.SelectedIndex)
            {
                case 1:
                    list = list.OrderBy(i => i.CustomerFullName).ToList();
                    break;
                case 2:
                    list = list.OrderByDescending(i => i.CustomerFullName).ToList();
                    break;
                default:
                    list = list.OrderBy(i => i.IdCust).ToList();
                    break;
            }

            list = list.
                Skip(15 * pageNum).
                Take(15).
                ToList();

            LVClient.ItemsSource = list;

        }

        private void BTNClear_Click(object sender, RoutedEventArgs e)
        {
            TBFIO.Text = "";
            TBAddress.Text = "";
            TBPhone.Text = "";
            CBSort.SelectedIndex = 0;
        }

        private void BTNAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClient = new AddClientWindow();
            addClient.ShowDialog();
            Filtr();
        }

        private void BTNEditClient_Click(object sender, RoutedEventArgs e)
        {
            if (LVClient.SelectedItem is Customer customer)
            {
                idCustomer = customer.IdCust;
                EditClientWindow edit = new EditClientWindow();
                edit.ShowDialog();
                LVClient.ItemsSource = Class.EF.Context.Customers.ToList();

            }
            else
            {
                MessageBox.Show("Выберите клиента из списка!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TBFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void TBPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
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

        private void TBAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void CBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }
    }
}
