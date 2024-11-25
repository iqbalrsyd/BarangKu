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
        private readonly int _userId = UserSessionService.Instance.User.UserId;
        private readonly UserService _userService;

        public EditProfileView()
        {
            InitializeComponent();
            DataContext = new EditProfileViewModel();
            _userService = new UserService();
            LoadUserProfile();

        }

        private void LoadUserProfile()
        {
            var user = _userService.GetUserById(_userId);
            if (user != null)
            {
                UsernameTextBox.Text = user.Username;
                FirstNameTextBox.Text = user.FirstName;
                LastNameTextBox.Text = user.LastName;
                TelephoneTextBox.Text = user.Telephone;
                EmailTextBox.Text = user.Email;
                AddressTextBox.Text = user.Address;
                LanguageComboBox.SelectedValue = user.Language;

                if (user.ProfilePicture != null)
                {
                    var bitmap = new BitmapImage();
                    using (var stream = new System.IO.MemoryStream(user.ProfilePicture))
                    {
                        bitmap.BeginInit();
                        bitmap.StreamSource = stream;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                    }
                    ProfilePictureImage.ImageSource = bitmap;
                }
            }
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

            var user = new UserModel
            {
                UserId = _userId,
                Username = username,
                FirstName = firstname,
                LastName = lastname,
                Telephone = telephone,
                Email = email,
                Address = address,
                Language = language,
                ProfilePicture = _profileImageData
            };

            UserService userService = new UserService();
            userService.UpdateUser(user);

            EditProfileViewModel editProfile = new EditProfileViewModel();
            UserModel userModel = editProfile.EditInfoUser(userid, username, firstname, lastname, email, telephone, address, language);
            if (userModel != null)
            {
               
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

        private void ChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    _profileImageData = new byte[stream.Length];
                    stream.Read(_profileImageData, 0, (int)stream.Length);
                }

                // Menampilkan gambar di UI
                BitmapImage bitmap = new BitmapImage(new Uri(filePath));
                ProfilePictureImage.ImageSource = bitmap;
            }
        }



    }
}
