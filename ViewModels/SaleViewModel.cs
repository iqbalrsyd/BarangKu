using BarangKu.Models;
using BarangKu.Services;
using Npgsql;
using System.ComponentModel;
using System.Windows;

namespace BarangKu.ViewModels
{
    public class SaleViewModel : INotifyPropertyChanged
    {
        private Sale _sale;
        private readonly SaleService _saleService;
        private readonly DatabaseService _dbService;

        public SaleViewModel()
        {
            _sale = new Sale();
            _saleService = new SaleService();
            _dbService = new DatabaseService();
        }

        public Sale Sale
        {
            get { return _sale; }
            set
            {
                _sale = value;
                OnPropertyChanged("Sale");
            }
        }

        public void RecordSale(Product product, int quantity, decimal totalAmount)
        {
            _saleService.RecordSale(_sale, product, quantity, totalAmount);
            OnPropertyChanged("Sale");
        }

        public void TrackShipment()
        {
            _saleService.TrackShipment(_sale);
            OnPropertyChanged("Sale");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
