using BarangKu.Models;
using BarangKu.Services;
using Microsoft.Win32;
using Npgsql;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace BarangKu.ViewModels
{
    public class EditProfileViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private byte[] _profilePicture;
        private UserModel _user;
        public int UserId { get; set; } = 1; // ID pengguna aktif


        //public string Username { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        //public string Email { get; set; }
        //public string Address { get; set; }
        //public string Language { get; set; }
        private readonly DatabaseService _dbService;

        public EditProfileViewModel(int userId)
        {
            // Initialize services and fields
            _userService = new UserService();
            _user = _userService.GetUserById(userId);
        }

       

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
            GetInfoUser();
            _dbService = new DatabaseService();

            ChangePhotoCommand = new RelayCommand(ChangePhoto);
            //SaveProfileCommand = new RelayCommand(SaveProfile);
        }

        //private void LoadUserData()
        //{
        //    var user = _userService.GetUserById(UserId);
        //    if (user != null)
        //    {
        //        Username = user.Username;
        //        FirstName = user.FirstName;
        //        LastName = user.LastName;
        //        PhoneNumber = user.Telephone;
        //        Email = user.Email;
        //        Address = user.Address;
        //        Language = user.Language;
        //        ProfilePicture = user.ProfilePicture;
        //    }
        //}


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

        //private void SaveProfile()
        //{
        //    var user = new UserModel
        //    {
        //        UserId = UserId,
        //        Username = Username,
        //        FirstName = FirstName,
        //        LastName = LastName,
        //        Telephone = PhoneNumber,
        //        Email = Email,
        //        Address = Address,
        //        Language = Language,
        //        ProfilePicture = ProfilePicture
        //    };

        //    _userService.UpdateUser(user);
        //}

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

        private string _username;
        private string _firstName;
        private string _lastName;
        private string _telephone;
        private string _email;
        private string _address;
        private string _language;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Telephone
        {
            get => _telephone;
            set
            {
                _telephone = value;
                OnPropertyChanged(nameof(Telephone));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string Language
        {
            get => _language;
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }

        public void GetInfoUser()
        {
            if(UserSessionService.Instance.User != null)
            {
                Username = UserSessionService.Instance.User.Username;
                FirstName = UserSessionService.Instance.User.FirstName;
                LastName = UserSessionService.Instance.User.LastName;
                Telephone = UserSessionService.Instance.User.Telephone;
                Email = UserSessionService.Instance.User.Email;
                Address = UserSessionService.Instance.User.Address;
                Language = UserSessionService.Instance.User.Language;

            }
        }

        public UserModel EditInfoUser(int userid, string username, string firstname, string lastname, string email, string telephone, string address, string language, byte[] profilePicture)
        {
            var conn = _dbService.GetConnection();
            UserModel updateUser = UserSessionService.Instance.User;

            try
            {
                string update = @"UPDATE users SET username = @username, firstname = @firstname, lastname = @lastname, email = @email, 
                        telephone = @telephone, address = @address, language = @language, profile_picture = @profilePicture WHERE userid = @userid";

                using (var cmd = new NpgsqlCommand(update, conn))
                {
                    cmd.Parameters.AddWithValue("userid", userid);
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("firstname", firstname);
                    cmd.Parameters.AddWithValue("lastname", lastname);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("telephone", telephone);
                    cmd.Parameters.AddWithValue("address", address);
                    cmd.Parameters.AddWithValue("language", language);
                    cmd.Parameters.AddWithValue("profilePicture", profilePicture ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        updateUser = new UserModel
                        {
                            UserId = userid,
                            Username = username,
                            FirstName = firstname,
                            LastName = lastname,
                            Email = email,
                            Telephone = telephone,
                            Address = address,
                            Language = language,
                            ProfilePicture = profilePicture
                        };
                        UserSessionService.Instance.User = updateUser;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saat memperbarui data user: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }

            return updateUser;
        }



    }
}
