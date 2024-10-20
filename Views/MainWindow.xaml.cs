using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BarangKu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Uri berandaView = new Uri("/Views/BerandaView.xaml", UriKind.Relative);
            mainFrame.NavigationService.Navigate(berandaView);

            Uri iconRighView = new Uri("/Views/IconRightView.xaml", UriKind.Relative);
            iconRightFrame.NavigationService.Navigate(iconRighView);
        }
    }
}