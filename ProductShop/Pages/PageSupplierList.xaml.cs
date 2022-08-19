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
    /// Логика взаимодействия для PageSupplierList.xaml
    /// </summary>
    public partial class PageSupplierList : Page
    {
        int pageNum = 0;
        List<Supplier> list = new List<Supplier>();

        public PageSupplierList()
        {
            InitializeComponent();
            LVSupplier.ItemsSource = Class.EF.Context.Suppliers.ToList().OrderBy(i => i.IdSupplier);

            CBSort.SelectedIndex = 0;
            CBSort.ItemsSource = new List<string>
            {
                "По умолчанию",
                "Наименование, А-Я",
                "Наименование, Я-А",
            };
        }

        public void Filtr()
        {
            // поиск
            list = Class.EF.Context.Suppliers.Where(i => i.NameSupplier.Contains(TBName.Text))
                .Where(i => i.Address.Contains(TBAddress.Text))
                .Where(i => i.Phone.Contains(TBPhone.Text))
                .ToList();

            // сортировка
            switch (CBSort.SelectedIndex)
            {
                case 1:
                    list = list.OrderBy(i => i.NameSupplier).ToList();
                    break;
                case 2:
                    list = list.OrderByDescending(i => i.NameSupplier).ToList();
                    break;
                default:
                    list = list.OrderBy(i => i.IdSupplier).ToList();
                    break;
            }

            list = list.
                Skip(15 * pageNum).
                Take(15).
                ToList();

            LVSupplier.ItemsSource = list;

        }

        private void BTNAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            AddSupplierWindow addSupplier = new AddSupplierWindow();
            addSupplier.ShowDialog();
            Filtr();
        }

        private void BTNEditSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (LVSupplier.SelectedItem is Supplier supplier)
            {
                idSupplier = supplier.IdSupplier;
                EditSupplierWindow edit = new EditSupplierWindow();
                edit.ShowDialog();
                Filtr();

            }
            else
            {
                MessageBox.Show("Выберите поставщика из списка!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TBFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void TBAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void TBPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void CBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        private void BTNClear_Click(object sender, RoutedEventArgs e)
        {
            TBName.Text = "";
            TBAddress.Text = "";
            TBPhone.Text = "";
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
    }
}
