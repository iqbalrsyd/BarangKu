using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace BarangKu.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        // Example properties
        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        // Example method to simulate loading the profile
        public void LoadProfile()
        {
            // You can replace this with actual logic to fetch user profile data
            UserName = "John Doe";
            Email = "john.doe@example.com";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

