using BarangKu.Services;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BarangKu.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        public ImageSource ProfilePictureImage { get; private set; }

        public ProfileViewModel()
        {
            _userService = new UserService();
            LoadProfile();
        }

        private void LoadProfile()
        {
            var user = _userService.GetUserById(1); // Sesuaikan ID
            if (user != null)
            {
                Username = user.Username;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Email = user.Email;
                PhoneNumber = user.Telephone;
                Address = user.Address;
                Language = user.Language;
                ProfilePictureImage = ConvertToImageSource(user.ProfilePicture);
            }
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
