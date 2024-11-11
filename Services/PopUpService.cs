using System.ComponentModel;
using System.Windows.Input;

namespace BarangKu.Services
{
    public class PopUpService : INotifyPropertyChanged
    {
        public static PopUpService _instance;
        public static PopUpService Instance => _instance ??= new PopUpService();

        public PopUpService() { } 

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                _isPopupOpen = value;
                OnPropertyChanged(nameof(IsPopupOpen));
            }
        }

        private object _popupContent;
        public object PopupContent
        {
            get => _popupContent;
            set
            {
                _popupContent = value;
                OnPropertyChanged(nameof(PopupContent));
            }
        }

        private ICommand _closePopupCommand;
        public ICommand ClosePopupCommand
        {
            get
            {
                if (_closePopupCommand == null)
                {
                    _closePopupCommand = new RelayCommand<object>(_ =>
                    {
                        IsPopupOpen = false;
                    });
                }
                return _closePopupCommand;
            }
        }

        public void ShowPopup(object viewContent)
        {
            PopupContent = viewContent;
            IsPopupOpen = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
