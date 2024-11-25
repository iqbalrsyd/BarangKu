using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace BarangKu.Models
{
    public class Cart: INotifyPropertyChanged
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public decimal Price {  get; set; }
        public BitmapImage ImageURL { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}