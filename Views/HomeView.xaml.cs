using BarangKu.Models;
using BarangKu.Services;
using BarangKu.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BarangKu.Views
{
    public partial class HomeView : UserControl
    {
        private ProductViewModel _productViewModel;

        public HomeView()
        {
            InitializeComponent();
            int userId = UserSessionService.Instance.User.UserId;

            _productViewModel = new ProductViewModel();
            DataContext = _productViewModel;

            LoadProducts(userId);
        }

        private void LoadProducts(int userId)
        {
            try
            {
                var products = _productViewModel.GetProductsForUser(userId);

                // Set the product list for the ViewModel
                _productViewModel.ProductList = new ObservableCollection<Product>(products);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat memuat produk: {ex.Message}");
            }
        }


        private void btnDetailProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.CommandParameter is Product selectedProduct)
                {
                    _productViewModel.SelectedProduct = selectedProduct;
                    ShowProductDetail(selectedProduct);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat membuka detail: {ex.Message}");
            }
        }

        private void ShowProductDetail(Product product)
        {
            if (product == null)
            {
                MessageBox.Show("Produk tidak valid.");
                return;
            }

            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow?.DataContext is NavigationServices navigationService)
            {
                //navigationService.ShowDetailProduct(product);
            }
            else
            {
                MessageBox.Show("Gagal membuka detail produk.");
            }
        }
    }
}
