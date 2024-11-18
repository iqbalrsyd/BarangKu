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
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using BarangKu.Services;
using BarangKu.Models;

namespace BarangKu.Views
{
    public partial class EditProfileView : UserControl
    {
        private readonly DatabaseService _databaseService = new DatabaseService();
        private byte[] _profileImageData; 
        private readonly int _userId = 1; 
        private UserModel _userModel;
        public EditProfileView()
        {
            InitializeComponent();
            DataContext = new EditProfileViewModel();

        }

        private void NavigateToProfileView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow.DataContext as NavigationServices;
            navigationService?.NavigateToProfileView();
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string firstname = FirstNameTextBox.Text;
            string lastname = LastNameTextBox.Text;
            string telephone = TelephoneTextBox.Text;   
            string email = EmailTextBox.Text;
            string address = AddressTextBox.Text;
            string language = (LanguageComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            int userid = UserSessionService.Instance.User.UserId;

            EditProfileViewModel editProfile = new EditProfileViewModel();
            UserModel userModel = editProfile.EditInfoUser(userid, username, firstname, lastname, email, telephone, address, language);
            if (userModel != null)
            {
                UserSessionService.Instance.User.Username = userModel.Username;
                UserSessionService.Instance.User.FirstName = userModel.FirstName;
                UserSessionService.Instance.User.LastName = userModel.LastName;
                UserSessionService.Instance.User.Email = userModel.Email;
                UserSessionService.Instance.User.Telephone = userModel.Telephone;
                UserSessionService.Instance.User.Address = userModel.Address;
                UserSessionService.Instance.User.Language = userModel.Language;
                MessageBox.Show("Data telah tersimpan!");
                var mainWindow = Window.GetWindow(this) as MainWindow;
                var navigationService = mainWindow?.DataContext as NavigationServices;
                navigationService?.NavigateToProfileView();
            }
            else
            {
                MessageBox.Show("Data gagal disimpan.");
            }
        }



    }
}
