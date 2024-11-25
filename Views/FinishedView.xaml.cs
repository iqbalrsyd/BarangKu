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
    /// Interaction logic for FinishedView.xaml
    /// </summary>
    public partial class FinishedView : UserControl
    {
        public FinishedView()
        {
            InitializeComponent();
            HistoryTransactionViewModel viewModel = new HistoryTransactionViewModel();
            DataContext = viewModel;
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

        private void NavigateToSentView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var navigationService = mainWindow.DataContext as NavigationServices;
                navigationService?.NavigateToSentView();
            }
        }
    }
}
