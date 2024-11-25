using BarangKu.Models;  // Pastikan namespace ini benar
using BarangKu.Services;
using BarangKu.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BarangKu.Views
{
    public partial class TransactionView : UserControl
    {
        private TransactionViewModel viewModel;

        public TransactionView()
        {
            InitializeComponent();

            HistoryTransactionViewModel viewModel = new HistoryTransactionViewModel();
            DataContext = viewModel;
        }


        private void NavigateToSentView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var navigationService = mainWindow.DataContext as NavigationServices;
                navigationService?.NavigateToSentView();
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

        private void KirimButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic untuk tombol "Kirim"
            MessageBox.Show("Pesanan berhasil dikirim");
        }

        private void NavigateToCartView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow.DataContext as NavigationServices;
            navigationService?.NavigateToCartView();
        }
    }

}

