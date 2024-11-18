using BarangKu.Models;
using BarangKu.Services;
using System.Windows;
using System.Windows.Controls;

namespace BarangKu.Views
{
    /// <summary>
    /// Interaction logic for DetailProductView.xaml
    /// </summary>
    public partial class DetailProductView : UserControl
    {
        public DetailProductView(Product selectedProduct)
        {
            InitializeComponent();
            DataContext = selectedProduct; // Mengatur DataContext langsung ke produk yang dipilih
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow?.DataContext as NavigationServices;
            navigationService?.ShowHome(); // Navigasi kembali ke halaman Home
        }
    }
}
