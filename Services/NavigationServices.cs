using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BarangKu.Models;
using BarangKu.ViewModels;
using BarangKu.Views;

namespace BarangKu.Services
{
    public class NavigationServices : INotifyPropertyChanged
    {

        public CollectionViewSource MenuItemsCollection;

        public ICollectionView SourceCollection => MenuItemsCollection.View;

        public NavigationServices()
        {
            ObservableCollection<MenuItemsModel> menuItems = new ObservableCollection<MenuItemsModel>
            {
                new MenuItemsModel { MenuName = "Beranda", MenuIcon = "/Assets/home.png", IsSelected=true},
                new MenuItemsModel { MenuName = "Artikel", MenuIcon = "/Assets/artikel.png"},
                new MenuItemsModel { MenuName = "Transaksi", MenuIcon = "/Assets/pengiriman.png"},
                new MenuItemsModel { MenuName = "Toko", MenuIcon = "/Assets/store.png"},
                new MenuItemsModel { MenuName = "Profil", MenuIcon = "/Assets/BsPerson.png"},
                new MenuItemsModel { MenuName = "Keluar", MenuIcon = "/Assets/keluar.png" }
            };

            MenuItemsCollection = new CollectionViewSource { Source = menuItems };
            SelectedViewModel = new HomeView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private object _selectedViewModel;
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        // Navigasi ke tampilan login saat logout
        public void Logout()
        {
            // Hapus data UserSession
            UserSessionService.Instance.User = null;
            UserSessionService.Instance.Seller = null;

            // Buka kembali UserEnterWindow untuk login ulang
            UserEnterWindow userEnterWindow = new UserEnterWindow();
            Application.Current.MainWindow = userEnterWindow;
            userEnterWindow.Show();

            // Tutup semua window yang ada
            foreach (var window in Application.Current.Windows)
            {
                if (window is Window currentWindow && currentWindow != userEnterWindow)
                {
                    currentWindow.Close();  // Menutup semua window selain UserEnterWindow
                }
            }

        }

        // pop  up untuk create store
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

        private object _popUpView;
        public object PopUpView
        {
            get => _popUpView;
            set
            {
                _popUpView = value;
                OnPropertyChanged(nameof(PopUpView));
            }
        }

        public void SwitchViews(object parameter)
        {
            foreach (var item in MenuItemsCollection.Source as ObservableCollection<MenuItemsModel>)
            {
                item.IsSelected = item.MenuName == parameter.ToString();
            }

            switch (parameter)
            {
                case "Beranda":
                    SelectedViewModel = new HomeView();
                    break;
                case "Artikel":
                    SelectedViewModel = new ArticleView();
                    break;
                case "Transaksi":
                    SelectedViewModel = new TransactionView();
                    break;
                case "Toko":
                    Authenticator authenticator = new Authenticator();
                    SelectedViewModel = new StoreView();
                    if (!authenticator.AccessStorePage())
                    {
                        PopUpView = new CreateStoreView();
                        IsPopupOpen = true;
                    }
                    break;
                case "Profil":
                    SelectedViewModel = new ProfileView();
                    break;
                case "EditProfil":
                    SelectedViewModel = new EditProfileView();
                    break;
                case "Keluar":
                    Logout();  // Logout dan navigasi ke login
                    break;
                default:
                    SelectedViewModel = new HomeView();
                    break;
            }
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        public void ClosePopup()
        {
            IsPopupOpen = false;
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        private ICommand _menucommand;
        public ICommand MenuCommand
        {
            get
            {
                if (_menucommand == null)
                {
                    _menucommand = new RelayCommand<object>(param => SwitchViews(param));
                }
                return _menucommand;
            }
        }

        public void ShowDetailProduct(Products selectedProduct)
        {
            SelectedViewModel = new DetailProductView(selectedProduct);
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        public void ShowHome()
        {
            SelectedViewModel = new HomeView();
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        public void AddProduct(MyProductView view = null)
        {
            int productid = 0;
            if (view == null)
            {
                // Jika view tidak diberikan (misalnya untuk tambah produk), buat instance baru
                var myProductViewModel = new MyProductViewModel(0); // 0 untuk produk baru
                view = new MyProductView(productid)
                {
                    DataContext = myProductViewModel
                };
            }

            // Atur SelectedViewModel ke MyProductView yang baru
            SelectedViewModel = view;
            OnPropertyChanged(nameof(SelectedViewModel));
        }


        public void NavigateToStoreView()
        {
            SelectedViewModel = new StoreView();
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        public void NavigateToEditProfileView()
        {
            SelectedViewModel = new EditProfileView();
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        public void NavigateToProfileView()
        {
            SelectedViewModel = new ProfileView();
            OnPropertyChanged(nameof(SelectedViewModel));
        }
        
        public void NavigateToSentView()
        {
            SelectedViewModel = new SentView();
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        public void NavigateToTransactionView()
        {
            SelectedViewModel = new TransactionView();
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        public void NavigateToFinishedView()
        {
            SelectedViewModel = new FinishedView();
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        private string _activeButton;
        public string ActiveButton
        {
            get => _activeButton;
            set
            {
                _activeButton = value;
                OnPropertyChanged();
            }
        }

        private bool _isOrderedVisible;
        public bool IsOrderedVisible
        {
            get => _isOrderedVisible;
            set
            {
                _isOrderedVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isSentVisible;
        public bool IsSentVisible
        {
            get => _isSentVisible;
            set
            {
                _isSentVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isFinishedVisible;
        public bool IsFinishedVisible
        {
            get => _isFinishedVisible;
            set
            {
                _isFinishedVisible = value;
                OnPropertyChanged();
            }
        }
        public void ShowOrdered()
        {
            ActiveButton = "Ordered";
            IsOrderedVisible = true;
            IsSentVisible = false;
            IsFinishedVisible = false;
        }

        public void ShowShipped()
        {
            ActiveButton = "Shipped";
            IsOrderedVisible = false;
            IsSentVisible = true;
            IsFinishedVisible = false;
        }

        public void ShowCompleted()
        {
            ActiveButton = "Completed";
            IsOrderedVisible = false;
            IsSentVisible = false;
            IsFinishedVisible = true;
        }
    }

}
