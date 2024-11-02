using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using BarangKu.Models;
using BarangKu.ViewModels;
using BarangKu.Views;

namespace BarangKu.Services
{
    public class NavigationServices: INotifyPropertyChanged
    {

        public CollectionViewSource MenuItemsCollection;

        public ICollectionView SourceCollection => MenuItemsCollection.View;

        public NavigationServices()
        {
            ObservableCollection<MenuItemsModel> menuItems = new ObservableCollection<MenuItemsModel>
            {
                new MenuItemsModel { MenuName = "Beranda", MenuIcon = "/Assets/home.png", IsSelected=true},
                new MenuItemsModel { MenuName = "Artikel", MenuIcon = "/Assets/artikel.png"},
                new MenuItemsModel { MenuName = "Transaksi", MenuIcon = "/Assets/pengiriman.png"}
            };

            MenuItemsCollection = new CollectionViewSource { Source = menuItems };
            SelectedViewModel = new HomeView();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
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
                    SelectedViewModel = new TransactionViewModel();
                    break;
                default:
                    SelectedViewModel = new HomeView();
                    break;
            }
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

    }
}
