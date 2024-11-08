using BarangKu.Models;
using BarangKu.Services;
using System.ComponentModel;
using BarangKu.ViewModels;
using Npgsql;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;

namespace BarangKu.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private Product _product;
        private readonly ProductService _productService;
        private readonly DatabaseService _dbService;

        public ProductViewModel()
        {
            _product = new Product();
            _productService = new ProductService();
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

        //public void AddProduct()
        //{
        //    _productService.AddProduct(_product);
        //    OnPropertyChanged("Product");
        //}

        public virtual Product AddProduct(int categoryId, string name, string description, decimal price, int stock, int duration, byte[] imageUrl)
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
                        CreatedDate = DateTime.Now
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
