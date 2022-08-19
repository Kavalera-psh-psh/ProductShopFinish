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
using System.Collections.ObjectModel;
using ProductShop.EF;
using ProductShop.Win;
using static ProductShop.Class.IDClass;
using static ProductShop.Class.EFStorekeeper;

namespace ProductShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageProductList.xaml
    /// </summary>
    public partial class PageProductList : Page
    {
        int pageNum = 0;

        List<EF.Product> list = new List<EF.Product>();
        
        public PageProductList()
        {
            InitializeComponent();
            
            CBSort.SelectedIndex = 0;
            CBSort.ItemsSource = new List<string>
            {
                "По умолчанию",
                "Наименование, А-Я",
                "Наименование, Я-А",
                "Цена (по возрастанию)",
                "Цена (по убыванию)",
            };
            Filtr();
        }

        public void Filtr()
        {
            list = Class.EF.Context.Products.ToList();

            // поиск
            list = Class.EF.Context.Products.Where(i => i.NameProd.Contains(txbNameProd.Text)).ToList();

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
                    list = list.OrderBy(i => i.IdProd).ToList();
                    break;
            }

            list = list.
                Skip(15 * pageNum).
                Take(15).
                ToList();

            lvProduct.ItemsSource = list;

        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            
            if (lvProduct.SelectedItem is Product product)
            {
                EditProductWindow editProduct = new EditProductWindow(product);
                editProduct.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите продукт из списка!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            Filtr();
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

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProd = new AddProductWindow();
            addProd.ShowDialog();
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

    }
}
