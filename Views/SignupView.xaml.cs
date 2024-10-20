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
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Validasi sederhana
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Semua kolom harus diisi!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Password tidak cocok!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Jika valid, tampilkan pesan sukses
            MessageBox.Show("Akun berhasil dibuat!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            NavigationService?.Navigate(loginView);
        }
    }
}
