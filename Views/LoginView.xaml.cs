using BarangKu.Models;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpView signUpView = new SignUpView();
            NavigationService?.Navigate(signUpView);
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            UserViewModel userLogin = new UserViewModel();
            string username = tbUsername.Text;
            string password = tbPassword.Password;

            // Validasi input username dan password
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username dan Password harus diisi.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Menghentikan proses login jika ada yang kosong
            }

            if (userLogin.Login(username, password))
            {
                MessageBox.Show("Login Berhasil");

                // Buka MainWindow
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Tutup UserEnterWindow (Window yang menampung LoginView)
                Window currentUserEnter = Window.GetWindow(this);
                currentUserEnter?.Close();
            }
            else
            {
                MessageBox.Show("Login Gagal");
            }
        }
    }
}
