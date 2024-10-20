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
    /// Interaction logic for BerandaView.xaml
    /// </summary>
    public partial class BerandaView : Page
    {
        public BerandaView()
        {
            InitializeComponent();
            var products = GetProducts();
            if (products.Count > 0)
            {
                ListProducts.ItemsSource = products;
            }
        }
        private List<Products> GetProducts()
        {
            return new List<Products>()
                {
                    new Products(1, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(2, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(3, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(4, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(5, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(6, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(7, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(8, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(9, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(10, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(11, "Jual", "Sepatu", 100000, "/Assets/sepatu.png", "Pogung", 2),
                };
        }

    }
}
