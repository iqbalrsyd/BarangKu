using BarangKu.Models;
using BarangKu.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BarangKu.ViewModels
{
    public class StoreViewModel : INotifyPropertyChanged
    {
        private Product _product;
        private readonly DatabaseService _dbService;
        public ObservableCollection<Product> Products { get; set; }

        public StoreViewModel()
        {
            _product = new Product();
            _dbService = new DatabaseService();
        }

        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        public virtual Product AddProduct(int categoryId, string name, string description, decimal price, int stock, string condition, string duration, byte[] imageUrl)
        {
            Product product = null;
            int sellerid = UserSessionService.Instance.Seller.SellerId;
            var conn = _dbService.GetConnection();

            try
            {
                int productid;
                bool isUnique = false;
                string status = "Tersedia";
                

                do
                {
                    Random random = new Random();
                    productid = random.Next(100, 1000);

                    string checkProductid = "SELECT COUNT(1) FROM product WHERE productid = @productid";
                    using (var checkCmd = new NpgsqlCommand(checkProductid, conn))
                    {
                        checkCmd.Parameters.AddWithValue("productid", productid);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        isUnique = (count == 0);
                    }
                } while (!isUnique);

                string createProduct = @"INSERT INTO product (productid, sellerid, categoryid, name, description, price, stock, condition, duration, status, imageurl, createdate) 
                                   VALUES (@productid, @sellerid, @categoryId, @name, @description, @price, @stock, @condition, @duration, @status, @imageUrl, NOW())";

                using (var cmd = new NpgsqlCommand(createProduct, conn))
                {
                    cmd.Parameters.AddWithValue("productid", productid);
                    cmd.Parameters.AddWithValue("sellerid", sellerid);
                    cmd.Parameters.AddWithValue("categoryId", categoryId);
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("price", price);
                    cmd.Parameters.AddWithValue("stock", stock);
                    cmd.Parameters.AddWithValue("condition", condition);
                    cmd.Parameters.AddWithValue("duration", duration);
                    cmd.Parameters.AddWithValue("status", status);
                    cmd.Parameters.AddWithValue("imageUrl", imageUrl ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();

                    product = new Product
                    {
                        ProductID = productid,
                        SellerID = sellerid,
                        CategoryID = categoryId,
                        Name = name,
                        Description = description,
                        Price = price,
                        Stock = stock,
                        Condition = condition,
                        Duration = duration,
                        Status = status,
                        ImageURL = imageUrl != null ? ByteArrayToImage(imageUrl) : null,
                        CreatedDate = DateTime.Now,
                        IsSelected = false
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat menyimpan produk: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }
            return product;
        }

        private Authenticator authenticator = new Authenticator();
        public List<Product> GetProducts()
        {
            int userid = UserSessionService.Instance.User.UserId;
            List<Product> products = new List<Product>();
            var conn = _dbService.GetConnection();

            if (authenticator.IsUserSeller(userid))
            {
                try
                {
                    int sellerid = UserSessionService.Instance.Seller.SellerId;
                    // Modifikasi query untuk memfilter berdasarkan sellerId
                    string query = @"SELECT productid, sellerid, categoryid, name, description, price, stock, condition, status, duration, imageurl, createdate 
                         FROM product
                         WHERE sellerid = @sellerid"; // Menambahkan kondisi untuk sellerId

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("sellerid", sellerid);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    ProductID = reader.GetInt32(0),
                                    SellerID = reader.GetInt32(1),
                                    CategoryID = reader.GetInt32(2),
                                    Name = reader.GetString(3),
                                    Description = reader.GetString(4),
                                    Price = reader.GetDecimal(5),
                                    Stock = reader.GetInt32(6),
                                    Condition = reader.GetString(7),
                                    Status = reader.GetString(8),
                                    Duration = reader.GetString(9),
                                    ImageURL = reader.IsDBNull(10) ? null : ByteArrayToImage((byte[])reader[10]),
                                    CreatedDate = reader.GetDateTime(11)
                                };

                                products.Add(product);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saat mengambil produk: {ex.Message}");
                }
                finally
                {
                    _dbService.CloseConnection(conn);
                }
            }

            return products;
        }

        private BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = memoryStream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                return image;
            }
        }

        private void LoadProducts()
        {
            var productList = GetProducts();
            Products = new ObservableCollection<Product>(productList);
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
            LoadProducts();
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
