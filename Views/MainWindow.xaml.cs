using BarangKu.Services;
using BarangKu.Views;
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
        private readonly NavigationServices _navigationServices;
        public MainWindow()
        {
            InitializeComponent();
            _navigationServices = new NavigationServices();
            DataContext = _navigationServices;

            var username = UserSessionService.Instance.User?.Username ?? "Guest";
            usernameTextBlock.Text = $"Welcome, {username}!";
        }

        private void Logout()
        {
            // Hapus data sesi pengguna
            UserSessionService.Instance.User = null;

            // Buka kembali UserEnterWindow untuk login ulang
            UserEnterWindow userEnterWindow = new UserEnterWindow();
            userEnterWindow.Show();

            // Tutup MainWindow saat ini
            this.Close();
        }

        // Event handler untuk tombol Logout
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationServices.Logout(); // Memanggil metode logout dari NavigationServices
        }

    }
}
