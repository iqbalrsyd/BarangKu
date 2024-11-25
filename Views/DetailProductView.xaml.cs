using BarangKu.Models;
using BarangKu.Services;
using BarangKu.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private ICommand _addProductCommand;
        public ICommand AddProductCommand
        {
            get
            {
                if (_addProductCommand == null)
                {
                    _addProductCommand = new RelayCommand<object>(param => AddToCart(param));
                }
                return _addProductCommand;
            }
        }

        private ICommand _buyNowCommand;
        public ICommand BuyNowCommand
        {
            get
            {
                if (_buyNowCommand == null)
                {
                    _buyNowCommand = new RelayCommand<object>(param => BuyNow(param));
                }
                return _buyNowCommand;
            }
        }

        private void AddToCart(object parameter)
        {
            int userid = UserSessionService.Instance.User.UserId;
            int quantity = int.Parse(StockTextBox.Text);
            if (parameter is int productId)
            {
                   
                    CartViewModel cartViewModel = new CartViewModel();

                    var isAdded = cartViewModel.AddCart(userid, productId, quantity);

                    if (isAdded != null)
                    {
                        MessageBox.Show("Produk berhasil ditambahkan ke keranjang.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Gagal menambahkan ke keranjang, coba lagi.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                
            }
            else
            {
                MessageBox.Show("Invalid Product ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        

        private void BuyNow(object parameter)
        {
            int userid = UserSessionService.Instance.User.UserId;
            int quantity = int.Parse(StockTextBox.Text);
            if (parameter is int productId)
            {

                CartViewModel cartViewModel = new CartViewModel();

                var isAdded = cartViewModel.AddCart(userid, productId, quantity);

                if (isAdded != null)
                {

                    // Assuming you have a method to navigate back to StoreView
                    var mainWindow = Window.GetWindow(this) as MainWindow;
                    var navigationService = mainWindow?.DataContext as NavigationServices;
                    navigationService?.NavigateToCartView();
                }
                else
                {
                    MessageBox.Show("Gagal. silakan coba lagi.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Invalid Product ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
