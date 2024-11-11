using BarangKu.Services;
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
        //public static Frame MainFrameInstance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            var username = UserSessionService.Instance.User.Username;
            var email = UserSessionService.Instance.User.Email;
            usernameTextBlock.Text = $"Welcome, {email}!";

            //Loaded += MainWindow_Loaded;
            //MainFrameInstance = mainFrame;
        }

    }
}
