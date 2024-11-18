using BarangKu.Models;
using BarangKu.Services;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BarangKu.ViewModels
{
    public class EditProfileViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private byte[] _profilePicture;

        public int UserId { get; set; } = 1; // ID pengguna aktif

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }

        public byte[] ProfilePicture
        {
            get => _profilePicture;
            set
            {
                _profilePicture = value;
                OnPropertyChanged(nameof(ProfilePicture));
                ProfilePictureImage = ConvertToImageSource(value);
            }
        }

        private ImageSource _profilePictureImage;
        public ImageSource ProfilePictureImage
        {
            get => _profilePictureImage;
            private set
            {
                _profilePictureImage = value;
                OnPropertyChanged(nameof(ProfilePictureImage));
            }
        }

        public ICommand ChangePhotoCommand { get; }
        public ICommand SaveProfileCommand { get; }

        public EditProfileViewModel()
        {
            _userService = new UserService();
            LoadUserData();

            ChangePhotoCommand = new RelayCommand(ChangePhoto);
            SaveProfileCommand = new RelayCommand(SaveProfile);
        }

        private void LoadUserData()
        {
            var user = _userService.GetUserById(UserId);
            if (user != null)
            {
                Username = user.Username;
                FirstName = user.FirstName;
                LastName = user.LastName;
                PhoneNumber = user.Telephone;
                Email = user.Email;
                Address = user.Address;
                Language = user.Language;
                ProfilePicture = user.ProfilePicture;
            }
        }


        private void ChangePhoto()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                ProfilePicture = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void SaveProfile()
        {
            var user = new UserModel
            {
                UserId = UserId,
                Username = Username,
                FirstName = FirstName,
                LastName = LastName,
                Telephone = PhoneNumber,
                Email = Email,
                Address = Address,
                Language = Language,
                ProfilePicture = ProfilePicture
            };

            _userService.UpdateUser(user);
        }

        private ImageSource ConvertToImageSource(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            using (var ms = new MemoryStream(imageData))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
