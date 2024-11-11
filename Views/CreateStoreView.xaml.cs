using BarangKu.Models;
using BarangKu.Services;
using BarangKu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for CreateStoreView.xaml
    /// </summary>
    public partial class CreateStoreView : UserControl
    {
        public CreateStoreView()
        {
            InitializeComponent();
        }

        private void JoinSeller_Click(object sender, RoutedEventArgs e)
        {
            UserViewModel joinSeller = new UserViewModel();
            string storeName = StoreNameTextBox.Text;
            string storeDescription = DescriptionStoreTextBox.Text;

            SellerModel newSeller = joinSeller.JoinSeller(storeName, storeDescription);

            if(newSeller != null)
            {
                MessageBox.Show("Selamat! Toko Anda berhasil dibuat");
                var mainWindow = Window.GetWindow(this) as MainWindow;
                var navigationService = mainWindow?.DataContext as NavigationServices;
                navigationService?.NavigateToStoreView();
            }
            else
            {
                MessageBox.Show("Gagal Membuat Toko");
            }
        }
    }
}
