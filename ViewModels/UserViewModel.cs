using BarangKu.Models;
using BarangKu.Services;
using System.ComponentModel;

namespace BarangKu.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private UserModel _user;
        private readonly Authenticator _authenticator;

        public UserViewModel()
        {
            _user = new UserModel();
            _authenticator = new Authenticator();
        }

        public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }

        public bool Login(string username, string password)
        {
            return _authenticator.VerifyPassword(password, _user.HashedPassword);
        }

        public void Register(string username, string password, string firstName, string lastName, string telephone)
        {
            _user.Username = username;
            _user.HashedPassword = _authenticator.HashPassword(password);
            _user.FirstName = firstName;
            _user.LastName = lastName;
            _user.Telephone = telephone;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
