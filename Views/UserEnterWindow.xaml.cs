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
using System.Windows.Shapes;

namespace BarangKu.Views
{
    public partial class UserEnterWindow : Window
    {
        public UserEnterWindow()
        {
            InitializeComponent();
            Loaded += UserEnterWindow_Loaded;
        }

        public void UserEnterWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Navigasi ke halaman login (LoginView)
            Uri loginUri = new Uri("/Views/LoginView.xaml", UriKind.Relative);
            mainFrame.NavigationService.Navigate(loginUri);
        }

        public void Logout()
        {
            // Tutup MainWindow saat ini
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Close();
            }

            // Buka kembali UserEnterWindow untuk login ulang
            UserEnterWindow userEnterWindow = new UserEnterWindow();
            userEnterWindow.Show();
        }
    }
}
