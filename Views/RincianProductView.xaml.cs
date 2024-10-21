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
    public partial class RincianProdukView : Page
    {
        private ProductViewModel _productViewModel;
        public RincianProdukView(ProductViewModel productViewModel)
        {
            InitializeComponent();
            DataContext = productViewModel; // Pastikan DataContext terisi
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void GoBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack(); // Navigasi kembali ke halaman sebelumnya
            }
        }
    }
}