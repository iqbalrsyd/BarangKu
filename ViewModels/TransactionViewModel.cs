using BarangKu.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BarangKu.ViewModels
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TransactionItem> OrderedItems { get; set; }
        public ObservableCollection<TransactionItem> ShippedItems { get; set; }
        public ObservableCollection<TransactionItem> CompletedItems { get; set; }

        public TransactionViewModel()
        {
            // Initialize collections
            OrderedItems = new ObservableCollection<TransactionItem>
            {
                new TransactionItem { Id = "#3245678", Description = "Deskripsi produk", Quantity = 1, Price = 500000, ImagePath = "/Assets/sample_shoes.jpg" }
            };
            ShippedItems = new ObservableCollection<TransactionItem>();
            CompletedItems = new ObservableCollection<TransactionItem>();
        }

        // Command to move items from Ordered to Shipped
        public void MoveToShipped(TransactionItem item)
        {
            if (OrderedItems.Contains(item))
            {
                OrderedItems.Remove(item);
                ShippedItems.Add(item);
                OnPropertyChanged(nameof(OrderedItems));
                OnPropertyChanged(nameof(ShippedItems));
            }
        }

        // Command to move items from Shipped to Completed
        public void MoveToCompleted(TransactionItem item)
        {
            if (ShippedItems.Contains(item))
            {
                ShippedItems.Remove(item);
                CompletedItems.Add(item);
                OnPropertyChanged(nameof(ShippedItems));
                OnPropertyChanged(nameof(CompletedItems));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void MoveToShipped(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }

    public class TransactionItem : INotifyPropertyChanged
    {
        private string _id;
        private string _description;
        private int _quantity;
        private double _price;
        private string _imagePath;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
