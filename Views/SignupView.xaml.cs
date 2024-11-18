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
            // Ambil nilai dari textbox dan passwordbox
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string email = EmailTextBox.Text;
            string telephone = TelephoneTextBox.Text;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Validasi form
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(telephone) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Semua kolom harus diisi.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validasi format email
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email tidak valid.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validasi format telepon (misal: harus berupa angka)
            if (!IsValidPhoneNumber(telephone))
            {
                MessageBox.Show("Nomor telepon tidak valid. Minimal 10 angka", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validasi password dan confirm password
            if (password != confirmPassword)
            {
                MessageBox.Show("Password dan konfirmasi password tidak cocok.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validasi panjang password minimal 8 karakter
            if (password.Length < 8)
            {
                MessageBox.Show("Password harus memiliki minimal 8 karakter.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Pendaftaran user
            UserViewModel userRegister = new UserViewModel();
            UserModel newUser = userRegister.Register(username, password, firstName, lastName, email, telephone);

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

        // Validasi format email
        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Validasi format nomor telepon (hanya angka dan minimal 10 digit)
        private bool IsValidPhoneNumber(string telephone)
        {
            return telephone.All(char.IsDigit) && telephone.Length >= 10;
        }


        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            NavigationService?.Navigate(loginView);  
           
        }
    }
}
