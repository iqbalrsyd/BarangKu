using BarangKu.Models;
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

namespace BarangKu.Views
{
    /// <summary>
    /// Interaction logic for DetailProductView.xaml
    /// </summary>
    public partial class DetailProductView : Page
    {
        public Products product { get; set; }
        public DetailProductView(Products product)
        {
            InitializeComponent();

            this.product = product;
            DataContext = product;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BerandaView berandaView = new BerandaView();
            NavigationService?.Navigate(berandaView);
        }
    }
}
