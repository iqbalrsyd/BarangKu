using System.Windows;
using System.Windows.Controls;
using BarangKu.Models;
using BarangKu.Services;
using BarangKu.ViewModels;


namespace BarangKu.Views
{
    public partial class SeeMoreView : UserControl
    {
        public SeeMoreView(int articleId)
        {
            InitializeComponent();
            this.DataContext = new SeeMoreViewModel(articleId);
        }

        private void NavigateToArticleView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow.DataContext as NavigationServices;
            navigationService?.NavigateToArticleView();
        }

        private void NavigateToCartView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow.DataContext as NavigationServices;
            navigationService?.NavigateToCartView();
        }
    }
}
