using BarangKu.Models;
using BarangKu.Services;
using BarangKu.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BarangKu.Views
{
    /// <summary>
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : UserControl
    {
        private readonly CartViewModel _cartViewModel;

        public CartView()
        {
            InitializeComponent();

            // Inisialisasi ViewModel
            _cartViewModel = new CartViewModel();
            DataContext = _cartViewModel;

            // Muat data keranjang
            Loaded += CartView_Loaded;
        }

        private void CartView_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCart();
        }

        private void LoadCart()
        {
            try
            {
                // Dapatkan userId dari sesi pengguna
                int userId = UserSessionService.Instance.User.UserId;

                // Panggil metode ViewModel untuk memuat data keranjang
                _cartViewModel.LoadCartItems(userId);
            }
            catch (Exception ex)
            {
                // Tampilkan pesan error jika terjadi kesalahan
                MessageBox.Show($"Error saat memuat keranjang: {ex.Message}", "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
