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
        // Variabel global untuk menyimpan stok maksimal
        // Contoh nilai stok maksimal

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            // Mendapatkan nilai saat ini dari StockTextBox
            if (int.TryParse(StockTextBox.Text, out int currentStock))
            {
                // Mengurangi nilai, tetapi menjaga agar tidak kurang dari 1
                currentStock = Math.Max(1, currentStock - 1);
                StockTextBox.Text = currentStock.ToString();
            }
            else
            {
                // Jika parsing gagal, set nilai default ke 1
                StockTextBox.Text = "1";
            }
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            // Casting DataContext ke model Product
            if (DataContext is Product selectedProduct)
            {
                int stockLimit = selectedProduct.Stock; // Ambil stok dari DataContext

                if (int.TryParse(StockTextBox.Text, out int currentStock))
                {
                    if (currentStock < stockLimit) // Periksa batas stok
                    {
                        currentStock++;
                        StockTextBox.Text = currentStock.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Maksimal jumlah sudah tercapai!", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    StockTextBox.Text = "1"; // Jika parsing gagal, atur nilai awal ke 1
                }
            }
            else
            {
                MessageBox.Show("Produk tidak ditemukan dalam konteks data.", "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnTambahKeranjang_Click (object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow?.DataContext as NavigationServices;
            navigationService?.NavigateToCartView();
        }


    }
}
