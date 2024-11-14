using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using BarangKu.Services;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.Threading.Tasks;
using BarangKu.Models;

namespace BarangKu.ViewModels
{
    public class EditProfileViewModel : INotifyPropertyChanged
    {
        public string ProfileImagePath
        {
            get => _profile.ProfileImagePath;
            set
            {
                _profile.ProfileImagePath = value;
                OnPropertyChanged();
            }
        }

        private readonly EditProfileService _profileService;
        private EditProfileModel _profile;

        public EditProfileViewModel()
        {
            _profileService = new EditProfileService();
            LoadProfileCommand = new RelayCommand(async () => await LoadProfileAsync());
            SaveProfileCommand = new RelayCommand(async () => await SaveProfileAsync());
            ChangePhotoCommand = new RelayCommand(ChangePhoto);
            _profile = new EditProfileModel();
        }

        public ICommand LoadProfileCommand { get; }
        public ICommand SaveProfileCommand { get; }
        public ICommand ChangePhotoCommand { get; }

        public string Username
        {
            get => _profile.Username;
            set { _profile.Username = value; OnPropertyChanged(); }
        }

        // Define other properties here...

        private async Task LoadProfileAsync()
        {
            _profile = await _profileService.LoadProfileAsync();
            OnPropertyChanged(null); // Refresh all bindings
        }

        private async Task SaveProfileAsync()
        {
            await _profileService.SaveProfileAsync(_profile);
        }

        private void ChangePhoto()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select a Profile Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _profile.ProfileImagePath = openFileDialog.FileName;
                OnPropertyChanged(nameof(ProfileImagePath));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}