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
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : Page
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string email = EmailTextBox.Text;
            string telephone = TelephoneTextBox.Text;
            string confirmPassword = ConfirmPasswordBox.Password;
            S
            UserModel newUser = userService.CreateUser(username, password, firstName, lastName, email, telephone);

            if (newUser != null)
            {
                MessageBox.Show("User berhasil dibuat");
                LoginView loginView = new LoginView();
                NavigationService?.Navigate(loginView);

            }
            else
            {
                MessageBox.Show("Gagal Membuat Akun");
            }
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            
           
        }
    }
}
