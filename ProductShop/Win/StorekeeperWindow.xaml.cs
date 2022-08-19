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
using ProductShop.Win;
using ProductShop.Pages;
using ProductShop.Class;
using static ProductShop.Class.FrameClass;

namespace ProductShop.Win
{
    /// <summary>
    /// Логика взаимодействия для StorekeeperWindow.xaml
    /// </summary>
    public partial class StorekeeperWindow : Window
    {
        public StorekeeperWindow()
        {
            InitializeComponent();
            frame = FRMain;
            frame.Navigate(new PageProductList());
        }

        private void btnSupplierList_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageSupplierList());
        }

        private void btnSupplyList_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageSupplyList());
        }

        private void btnProductList_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageProductList());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }
    }
}
