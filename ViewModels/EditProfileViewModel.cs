using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using System.ComponentModel;
using BarangKu.Services;

namespace BarangKu.ViewModels
{
    public class EditProfileViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private byte[] _profilePicture;

        public byte[] ProfilePicture
        {
            get => _profilePicture;
            set
            {
                _profilePicture = value;
                OnPropertyChanged(nameof(ProfilePicture));
            }
        }

        public ICommand ChangePhotoCommand { get; }
        public ICommand SavePhotoCommand { get; }

        public EditProfileViewModel()
        {
            _userService = new UserService();
            ChangePhotoCommand = new RelayCommand(ChangePhoto);
            SavePhotoCommand = new RelayCommand(SavePhoto);
            LoadProfilePicture();
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

        private void SavePhoto()
        {
            _userService.UpdateProfilePicture(1, ProfilePicture); // Sesuaikan ID pengguna
        }

        private void LoadProfilePicture()
        {
            ProfilePicture = _userService.GetProfilePicture(1); // Sesuaikan ID pengguna
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
