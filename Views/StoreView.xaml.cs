using BarangKu.Services;
using BarangKu.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using BarangKu.Models;
using System.Windows.Input;

namespace BarangKu.Views
{
    /// <summary>
    /// Interaction logic for StoreView.xaml
    /// </summary>
    public partial class StoreView : UserControl
    {
        public StoreViewModel StoreViewModel { get; }
        public int SelectedProductId { get; set; }

        public StoreView()
        {
            InitializeComponent();
            StoreViewModel = new StoreViewModel();
            StoreViewModel.InitializeStoreData();
            DataContext = this;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow?.DataContext as NavigationServices;
            navigationService?.AddProduct();
        }

        private ICommand _editProductCommand;
        public ICommand EditProductCommand
        {
            get
            {
                if (_editProductCommand == null)
                {
                    _editProductCommand = new RelayCommand<object>(param => EditProduct(param));
                }
                return _editProductCommand;
            }
        }


        private void EditProduct(object parameter)
        {
            if (parameter is int productId)
            {
                // Buat instance ViewModel dengan ID produk
                var myProductViewModel = new MyProductViewModel(productId);

                var myProductView = new MyProductView(productId)
                {
                    DataContext = myProductViewModel
                };

                // Menggunakan metode mirip seperti AddProduct_Click
                var mainWindow = Window.GetWindow(this) as MainWindow;
                var navigationService = mainWindow?.DataContext as NavigationServices;

                if (navigationService != null)
                {
                    navigationService.AddProduct(myProductView);
                }
                else
                {
                    MessageBox.Show("NavigationServices tidak ditemukan di MainWindow.");
                }
            }
            else
            {
                MessageBox.Show("Invalid Product ID");
            }
        }


        private ICommand _deleteProductCommand;
        public ICommand DeleteProductCommand
        {
            get
            {
                if (_deleteProductCommand == null)
                {
                    _deleteProductCommand = new RelayCommand<object>(param => DeleteProduct(param));
                }
                return _deleteProductCommand;
            }
        }

        private void DeleteProduct(object parameter)
        {
            if (parameter is int productId)
            {
                // Confirm deletion with Yes/No dialog
                var result = MessageBox.Show("Apakah Anda yakin ingin menghapus produk ini?", "Konfirmasi Penghapusan", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var myProductViewModel = new MyProductViewModel(productId);

                    bool isDeleted = myProductViewModel.DeleteProduct(productId);

                    if (isDeleted)
                    {
                        MessageBox.Show("Produk berhasil dihapus.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Assuming you have a method to navigate back to StoreView
                        var mainWindow = Window.GetWindow(this) as MainWindow;
                        var navigationService = mainWindow?.DataContext as NavigationServices;
                        navigationService?.NavigateToStoreView();
                    }
                    else
                    {
                        MessageBox.Show("Gagal menghapus produk. Produk mungkin tidak ada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // If user chooses "No", do nothing (i.e., cancel the deletion)
                    MessageBox.Show("Penghapusan produk dibatalkan.", "Batal", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Invalid Product ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow?.DataContext as NavigationServices;
            navigationService?.NavigateToMyOrderView();
        }
    }
}
