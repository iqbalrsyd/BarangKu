using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarangKu.Models
{
    public class MenuItemsModel : INotifyPropertyChanged
    {
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }

        private bool _isSelected; // Ubah ini ke private

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value) // Pengecekan untuk perubahan
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
