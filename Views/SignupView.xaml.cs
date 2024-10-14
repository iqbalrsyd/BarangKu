using System.Windows;

namespace Barangku.Views
{
    public partial class SignupView : Window
    {
        public SignupView()
        {
            InitializeComponent();
        }

        // Event handler untuk tombol "Create"
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

        // Event handler untuk hyperlink "Log In"
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navigasi ke halaman Log In", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
