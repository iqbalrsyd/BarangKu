using BarangKu.Models;
using BarangKu.Services;
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
            UserService userService = new UserService();
            string username = tbUsername.Text;
            string password = tbPassword.Password;

            if (userService.Login(username, password))
            {
                MessageBox.Show("Login Berhasil");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window currentUserEnter = Window.GetWindow(this);
                currentUserEnter.Close();
            }
            else
            {
                MessageBox.Show("Login Gagal");
            }
        }
    }
}
