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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        private readonly DatabaseService _databaseService = new DatabaseService();
        private byte[] _profileImageData;
        private readonly int _userId = UserSessionService.Instance.User.UserId;
        private readonly UserService _userService = new UserService();

        public ProfileView()
        {
            InitializeComponent();
            EditProfileViewModel viewModel = new EditProfileViewModel();
            DataContext = viewModel;
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

        private void NavigateToEditProfileView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow.DataContext as NavigationServices;
            navigationService?.NavigateToEditProfileView();
        }

        private void NavigateToCartView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow.DataContext as NavigationServices;
            navigationService?.NavigateToCartView();
        }
    }
}
