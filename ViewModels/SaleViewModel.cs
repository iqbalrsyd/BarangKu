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

        private string _storeName;
        private string _storeDescription;
        private double _rating;

        public string StoreName
        {
            get => _storeName;
            set
            {
                _storeName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }

        public string StoreDescription
        {
            get => _storeDescription;
            set
            {
                _storeDescription = value;
                OnPropertyChanged(nameof(StoreDescription));
            }
        }

        public double Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        public void InitializeStoreData()
        {
            GetInfoStore();
            LoadStoreData();
        }

        public void LoadStoreData()
        {
            if (UserSessionService.Instance.Seller != null)
            {
                StoreName = UserSessionService.Instance.Seller.StoreName;
                StoreDescription = UserSessionService.Instance.Seller.StoreDescription;
                Rating = UserSessionService.Instance.Seller.Rating;
            }
        }
        public void GetInfoStore()
        {
            var conn = _dbService.GetConnection();
            int userid = UserSessionService.Instance.User.UserId;
            try
            {
                string get = "SELECT sellerid, storename, storedescription, rating, joindate FROM seller WHERE userid = @userid";
                using (var cmd = new NpgsqlCommand(get, conn))
                {
                    cmd.Parameters.AddWithValue("userid", userid);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int sellerid = reader.GetInt32(0);
                            string storeName = reader.GetString(1);
                            string storeDescription = reader.GetString(2);
                            double rating = reader.GetDouble(3);
                            DateTime joinDate = reader.GetDateTime(4);

                            UserSessionService.Instance.Seller = new SellerModel
                            {
                                SellerId = sellerid,
                                StoreName = storeName,
                                StoreDescription = storeDescription,
                                Rating = rating,
                                JoinDate = joinDate
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }
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
