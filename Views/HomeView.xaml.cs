using BarangKu.Models;
using BarangKu.Services;
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
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = this;
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
                    new Products(1, "Jual", "Sepatu", 100, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(2, "Jual", "Tas", 50, "/Assets/tas.jpg", "Godean", 1),
                    new Products(3, "Jual", "Rice Cooker", 200, "/Assets/rice_cooker.jpg", "Gejayan", 5),
                    new Products(1, "Jual", "Sepatu", 100, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(2, "Jual", "Tas", 50, "/Assets/tas.jpg", "Godean", 1),
                    new Products(3, "Jual", "Rice Cooker", 200, "/Assets/rice_cooker.jpg", "Gejayan", 5),
                    new Products(1, "Jual", "Sepatu", 100, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(2, "Jual", "Tas", 50, "/Assets/tas.jpg", "Godean", 1),
                    new Products(3, "Jual", "Rice Cooker", 200, "/Assets/rice_cooker.jpg", "Gejayan", 5),
                    new Products(1, "Jual", "Sepatu", 100, "/Assets/sepatu.png", "Pogung", 2),
                    new Products(2, "Jual", "Tas", 50, "/Assets/tas.jpg", "Godean", 1),
                    new Products(3, "Jual", "Rice Cooker", 200, "/Assets/rice_cooker.jpg", "Gejayan", 5),
                };
        }

        private void btnDetailProduct_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as Button;
            int productId = Convert.ToInt32(button.Tag);  

            var selectedProduct = GetProducts().FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    var navigationService = mainWindow.DataContext as NavigationServices;
                    navigationService?.ShowDetailProduct(selectedProduct);
                }
            }
        }

    }
}
