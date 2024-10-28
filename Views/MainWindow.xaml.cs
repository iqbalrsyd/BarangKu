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
        public static Frame MainFrameInstance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            MainFrameInstance = mainFrame;
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Uri berandaView = new Uri("/Views/BerandaView.xaml", UriKind.Relative);
            mainFrame.NavigationService.Navigate(berandaView);

            Uri iconRighView = new Uri("/Views/IconRightView.xaml", UriKind.Relative);
            iconRightFrame.NavigationService.Navigate(iconRighView);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri articleView = new Uri("/Views/ArticleView.xaml", UriKind.Relative);
            mainFrame.NavigationService.Navigate(articleView);


            Uri iconRighView = new Uri("/Views/IconRightView.xaml", UriKind.Relative);
            iconRightFrame.NavigationService.Navigate(iconRighView);
        }


        private void BerandaButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to BerandaView
            Uri berandaView = new Uri("/Views/BerandaView.xaml", UriKind.Relative);
            mainFrame.NavigationService.Navigate(berandaView);
        }


    }
}
