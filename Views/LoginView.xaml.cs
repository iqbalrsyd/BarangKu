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
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.LoginName = tbUsername.Text;
            user.Password = tbPassword.Password;
            if (user.Login(user.LoginName, user.Password))
            {
                MessageBox.Show("Login Berhasil, ID anda adalah " + user.EmployeeID.ToString());
            }
            else
            {
                MessageBox.Show("Login Gagal");
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignupView view = new SignupView();
            view.Show();
        }
    }
}
