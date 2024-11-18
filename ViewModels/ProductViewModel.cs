using BarangKu.Models;
using BarangKu.Services;
using System.ComponentModel;
using BarangKu.ViewModels;
using Npgsql;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BarangKu.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private Product _product;
        private readonly ProductService _productService;
        private readonly DatabaseService _dbService;
        private Product _selectedProduct;
        private ObservableCollection<Product> _productList;
        public ObservableCollection<Product> UserProductList { get; set; }

        public ProductViewModel()
        {
            _product = new Product();
            _productService = new ProductService();
            _dbService = new DatabaseService();
            UserProductList = new ObservableCollection<Product>();

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

        public ObservableCollection<Product> ProductList
        {
            get { return _productList; }
            set
            {
                _productList = value;
                OnPropertyChanged(nameof(ProductList));
            }
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }


        //public void AddProduct()
        //{
        //    _productService.AddProduct(_product);
        //    OnPropertyChanged("Product");
        //}

        public virtual Product AddProduct(int categoryId, string name, string description, decimal price, int stock, string duration, byte[] imageUrl)
        {
            Product product = null;
            int sellerid = 5446;
            var conn = _dbService.GetConnection();

            try
            {
                int productid;
                bool isUnique = false;

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

                string createProduct = @"INSERT INTO product (productid, sellerid, categoryid, name, description, price, stock, duration, imageurl, createdate) 
                                   VALUES (@productid, @sellerid, @categoryId, @name, @description, @price, @stock, @duration, @imageUrl, NOW())";

                using (var cmd = new NpgsqlCommand(createProduct, conn))
                {
                    cmd.Parameters.AddWithValue("productid", productid);
                    cmd.Parameters.AddWithValue("sellerid", sellerid);
                    cmd.Parameters.AddWithValue("categoryId", categoryId);
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("price", price);
                    cmd.Parameters.AddWithValue("stock", stock);
                    cmd.Parameters.AddWithValue("duration", duration);
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
                        Duration = duration,
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

        public List<Product> GetProducts()
        {
            int sellerId = UserSessionService.Instance.Seller.SellerId;
            List<Product> products = new List<Product>();
            var conn = _dbService.GetConnection();

            try
            {
                // Modifikasi query untuk memfilter berdasarkan sellerId
                string query = @"SELECT productid, sellerid, categoryid, name, description, price, stock, duration, imageurl, createdate 
                         FROM product
                         WHERE sellerid = @sellerId"; // Menambahkan kondisi untuk sellerId

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("sellerid", sellerId);

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
                                Duration = reader.GetString(7),
                                ImageURL = reader.IsDBNull(8) ? null : ByteArrayToImage((byte[])reader[8]),
                                CreatedDate = reader.GetDateTime(9)
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
            return products;
        }

        private BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            BitmapImage image = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                try
                {
                    image.BeginInit();
                    image.StreamSource = memoryStream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error converting image: {ex.Message}");
                    return null;
                }
            }
            return image;
        }


        public List<Product> GetProductsForUser(int userId)
        {
            List<Product> products = new List<Product>();
            var conn = _dbService.GetConnection();
            try
            {
                string query = @"SELECT productid, sellerid, categoryid, name, description, price, stock, duration, imageurl, createdate, condition
                         FROM product
                         WHERE sellerid != @userId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
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
                                Duration = reader.GetString(7),
                                ImageURL = reader.IsDBNull(8) ? null : ByteArrayToImage((byte[])reader[8]),
                                CreatedDate = reader.GetDateTime(9),
                                Condition = reader.GetString(10)
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
            return products;
        }

        public void EditProduct()
        {
            _productService.EditProduct(_product);
            OnPropertyChanged("Product");
        }

        public void DeleteProduct()
        {
            _productService.DeleteProduct(_product);
            OnPropertyChanged("Product");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
