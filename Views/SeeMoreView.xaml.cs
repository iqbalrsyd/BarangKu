using System.Windows;
using System.Windows.Controls;
using BarangKu.Models;
using BarangKu.Services; // Pastikan namespace model artikel

namespace BarangKu.Views
{
    public partial class SeeMoreView : UserControl
    {
        public SeeMoreView()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var navigationService = mainWindow.DataContext as NavigationServices;
                navigationService?.NavigateToArticleView();
            }
        }
    }
}
