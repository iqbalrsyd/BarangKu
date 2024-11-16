using BarangKu.Services;
using BarangKu.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using BarangKu.Models;

namespace BarangKu.Views
{
    /// <summary>
    /// Interaction logic for StoreView.xaml
    /// </summary>
    public partial class StoreView : UserControl
    {
        public StoreViewModel StoreViewModel { get; }     

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

    }
}
