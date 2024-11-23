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
    /// Interaction logic for MyOrderView.xaml
    /// </summary>
    public partial class MyOrderView : UserControl
    {
        public MyOrderView()
        {
            InitializeComponent();
            MyOrderViewModel orderViewModel = new MyOrderViewModel();
            DataContext = orderViewModel;
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
                
                var result = MessageBox.Show("Apakah Anda yakin ingin menyetujui transaksi ini?", "Konfirmasi Penghapusan", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var myOrderView = new MyOrderViewModel();

                    bool isUpdated = myOrderView.UpdateStatus(transactionid);

                    if (isUpdated)
                    {
                        MessageBox.Show("Transaksi berhasil disetujui.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Assuming you have a method to navigate back to StoreView
                        var mainWindow = Window.GetWindow(this) as MainWindow;
                        var navigationService = mainWindow?.DataContext as NavigationServices;
                        navigationService?.NavigateToMyOrderView();
                    }
                    else
                    {
                        MessageBox.Show("Gagal menyetujui transaksi.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // If user chooses "No", do nothing (i.e., cancel the deletion)
                    MessageBox.Show("Penyetujuan transaksi dibatalkan.", "Batal", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Invalid Transaction ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
