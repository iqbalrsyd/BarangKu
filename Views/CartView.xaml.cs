using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BarangKu.ViewModels;
using BarangKu.Services;

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
            //Loaded += CartView_Loaded;
        }

        private void NavigateToHomeView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow.DataContext as NavigationServices;
            navigationService?.NavigateToHomeView();
        }
    }
}
