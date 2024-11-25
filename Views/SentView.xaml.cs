using BarangKu.Services;
using BarangKu.ViewModels;
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
    /// Interaction logic for SentView.xaml
    /// </summary>
    public partial class SentView : UserControl
    {
        public SentView()
        {
            InitializeComponent();
            HistoryTransactionViewModel viewModel = new HistoryTransactionViewModel();
            DataContext = viewModel;
        }

        private ICommand _editStatusTransactionCommand;
        public ICommand EditStatusTransactionCommand
        {
            get
            {
                if (_editStatusTransactionCommand == null)
                {
                    _editStatusTransactionCommand = new RelayCommand<object>(param => UpdateStatusTransaction(param));
                }
                return _editStatusTransactionCommand;
            }
        }

        private void UpdateStatusTransaction(object parameter)
        {
            if (parameter is int transactionid)
            {

                var result = MessageBox.Show("Apakah Anda yakin ingin pesanan telah diterima?", "Konfirmasi Penghapusan", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var myOrderView = new HistoryTransactionViewModel();

                    bool isUpdated = myOrderView.UpdateStatus(transactionid);

                    if (isUpdated)
                    {
                        MessageBox.Show("Pesanan telah diterima.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Assuming you have a method to navigate back to StoreView
                        var mainWindow = Window.GetWindow(this) as MainWindow;
                        var navigationService = mainWindow?.DataContext as NavigationServices;
                        navigationService?.NavigateToFinishedView();
                    }
                    else
                    {
                        MessageBox.Show("Gagal menerima pesanan.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // If user chooses "No", do nothing (i.e., cancel the deletion)
                    MessageBox.Show("Penerimaan pesanan dibatalkan.", "Batal", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Invalid Transaction ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NavigateToTransactionView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var navigationService = mainWindow.DataContext as NavigationServices;
                navigationService?.NavigateToTransactionView();
            }
        }

        private void NavigateToFinishedView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var navigationService = mainWindow.DataContext as NavigationServices;
                navigationService?.NavigateToFinishedView();
            }
        }

        private void NavigateToCartView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow.DataContext as NavigationServices;
            navigationService?.NavigateToCartView();
        }
    }
}
