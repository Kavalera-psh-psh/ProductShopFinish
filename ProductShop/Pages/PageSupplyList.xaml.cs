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
    /// Логика взаимодействия для PageSupplyList.xaml
    /// </summary>
    public partial class PageSupplyList : Page
    {
        int pageNum = 0;
        List<Shipment> list = new List<Shipment>();
        public PageSupplyList()
        {
            InitializeComponent();
            LVSupply.ItemsSource = Class.EF.Context.Shipments.ToList();
            CBSort.SelectedIndex = 0;
            CBSort.ItemsSource = new List<string>
            {
                "По умолчанию",
                "Дата поставки (по возрастанию)",
                "Дата поставки (по убыванию)",
            };
        }

        public void Filtr()
        {

            // поиск
            list = Class.EF.Context.Shipments.Where(i => i.Supplier.NameSupplier.Contains(txbNameSupp.Text))
                .Where(i => i.Product.NameProd.Contains(txbNameProd.Text)).ToList();

            if (tbDateSupply.SelectedDate.HasValue)
            {
                list = list.Where(i => i.DateShipment == tbDateSupply.SelectedDate.Value).ToList();
            }

            // сортировка
            switch (CBSort.SelectedIndex)
            {
                case 1:
                    list = list.OrderBy(i => i.DateShipment).ToList();
                    break;
                case 2:
                    list = list.OrderByDescending(i => i.DateShipment).ToList();
                    break;
                default:
                    list = list.OrderBy(i => i.IdShipment).ToList();
                    break;
            }

            list = list.
                Skip(15 * pageNum).
                Take(15).
                ToList();

            LVSupply.ItemsSource = list;

        }



        private void txbNameSupp_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void txbNameProd_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void BTNAddSupply_Click(object sender, RoutedEventArgs e)
        {
            AddSupplyWindow addSupply = new AddSupplyWindow();
            addSupply.ShowDialog();
            Filtr();
        }

        private void BTNClear_Click(object sender, RoutedEventArgs e)
        {
            txbNameProd.Text = "";
            txbNameSupp.Text = "";
            tbDateSupply.Text = "";
            CBSort.SelectedIndex = 0;
        }

        private void CBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void tbDateSupply_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }
    }
}
