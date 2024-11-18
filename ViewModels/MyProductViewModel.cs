using BarangKu.Models;
using BarangKu.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace BarangKu.ViewModels
{
    public class MyProductViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _dbService;

        public ObservableCollection<Category> Categories { get; set; }

        public MyProductViewModel(int productid)
        {
            _dbService = new DatabaseService();
            LoadProductWithID(productid);
            LoadCategory();
        }

        private Product _product;
        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
                OnPropertyChanged(nameof(ProductName));
                OnPropertyChanged(nameof(ProductPrice));
                OnPropertyChanged(nameof(ProductDescription));
                OnPropertyChanged(nameof(ProductStock));
                OnPropertyChanged(nameof(ProductCondition));
                OnPropertyChanged(nameof(ProductDuration));
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public string ProductName
        {
            get => Product?.Name ?? string.Empty; // Berikan nilai default jika null
            set
            {
                if (Product == null)
                {
                    Product = new Product(); // Inisialisasi jika null
                }

                if (Product.Name != value)
                {
                    Product.Name = value;
                    OnPropertyChanged(nameof(ProductName));
                }
            }
        }

        public decimal ProductPrice
        {
            get => Product?.Price ?? 0; // Berikan nilai default jika null
            set
            {
                if (Product == null)
                {
                    Product = new Product(); // Inisialisasi jika null
                }

                if (Product.Price != value)
                {
                    Product.Price = value;
                    OnPropertyChanged(nameof(ProductPrice));
                }
            }
        }

        public string ProductDescription
        {
            get => Product?.Description ?? string.Empty; // Berikan nilai default jika null
            set
            {
                if (Product == null)
                {
                    Product = new Product(); // Inisialisasi jika null
                }

                if (Product.Description != value)
                {
                    Product.Description = value;
                    OnPropertyChanged(nameof(ProductDescription));
                }
            }
        }

        public int ProductStock
        {
            get => Product?.Stock ?? 1; // Berikan nilai default jika null
            set
            {
                if (Product == null)
                {
                    Product = new Product(); // Inisialisasi jika null
                }

                if (Product.Stock != value)
                {
                    Product.Stock = value;
                    OnPropertyChanged(nameof(ProductStock));
                }
            }
        }

        public string ProductCondition
        {
            get
            {
                return Product?.Condition switch
                {
                    "Baru" => "1",
                    "Preloved" => "2",
                    _ => string.Empty, // Default jika nilainya tidak dikenal
                };
            }
            set
            {
                if (Product == null)
                {
                    Product = new Product();
                }

                // Konversi kembali dari nilai Tag ke teks kondisi
                var newCondition = value switch
                {
                    "1" => "Baru",
                    "2" => "Preloved",
                    _ => string.Empty,
                };

                if (Product.Condition != newCondition)
                {
                    Product.Condition = newCondition;
                    OnPropertyChanged(nameof(ProductCondition));
                }
            }
        }


        public string ProductDuration
        {
            get => Product?.Duration ?? string.Empty; // Berikan nilai default jika null
            set
            {
                if (Product == null)
                {
                    Product = new Product(); // Inisialisasi jika null
                }

                if (Product.Duration != value)
                {
                    Product.Duration = value;
                    OnPropertyChanged(nameof(ProductDuration));
                }
            }
        }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => Product?.ImageURL ?? new BitmapImage(); // Berikan nilai default jika null
            set
            {
                if (Product == null)
                {
                    Product = new Product(); // Inisialisasi jika null
                }

                if (Product.ImageURL != value)
                {
                    Product.ImageURL = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }


        public void LoadProductWithID(int productid)
        {
            Product = GetProductWithId(productid);
        }

        public Product GetProductWithId(int productid)
        {
            var conn = _dbService.GetConnection();
            Product product = null;

            try
            {
                if (productid > 0)
                {
                    string getProduct = @"SELECT 
                                    p.productid, p.sellerid, p.categoryid, c.name, 
                                    p.name, p.description, p.price, p.stock, p.condition, 
                                    p.status, p.duration, p.imageurl, p.createdate
                                    FROM product p
                                    INNER JOIN category c ON p.categoryid = c.categoryid
                                    WHERE p.productid = @productid";
                    using (var cmd = new NpgsqlCommand(getProduct, conn))
                    {
                        cmd.Parameters.AddWithValue("productid", productid);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                product = new Product
                                {
                                    ProductID = reader.GetInt32(0),
                                    SellerID = reader.GetInt32(1),
                                    CategoryID = reader.GetInt32(2),
                                    CategoryName = reader.GetString(3),
                                    Name = reader.GetString(4),
                                    Description = reader.GetString(5),
                                    Price = reader.GetDecimal(6),
                                    Stock = reader.GetInt32(7),
                                    Condition = reader.GetString(8),
                                    Status = reader.GetString(9),
                                    Duration = reader.GetString(10),
                                    ImageURL = reader.IsDBNull(11) ? null : ByteArrayToImage((byte[])reader[11]),
                                    CreatedDate = reader.GetDateTime(12)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saat mengambil produk: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }

            return product;
        }

        public Product EditProduk(int productid, int categoryId, string name, string description, decimal price, int stock, string condition, string duration, byte[] imageUrl)
        {
            var conn = _dbService.GetConnection();
            Product updatedProduct = null;

            try
            {
                string updateProduct = @"UPDATE product 
                                 SET categoryid = @categoryid, name = @name, description = @description, price = @price, stock = @stock, 
                                     condition = @condition, duration = @duration, imageurl = @imageurl 
                                 WHERE productid = @productid";

                using (var cmd = new NpgsqlCommand(updateProduct, conn))
                {
                    cmd.Parameters.AddWithValue("productid", productid);
                    cmd.Parameters.AddWithValue("categoryid", categoryId);
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("price", price);
                    cmd.Parameters.AddWithValue("stock", stock);
                    cmd.Parameters.AddWithValue("condition", condition);
                    cmd.Parameters.AddWithValue("duration", duration);
                    cmd.Parameters.AddWithValue("imageurl", imageUrl ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // If the product is updated, return the updated Product object
                        updatedProduct = new Product
                        {
                            ProductID = productid,
                            CategoryID = categoryId,
                            Name = name,
                            Description = description,
                            Price = price,
                            Stock = stock,
                            Condition = condition,
                            Duration = duration,
                            ImageURL = imageUrl != null ? ByteArrayToImage(imageUrl) : null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saat memperbarui produk: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }

            return updatedProduct;
        }

        public bool DeleteProduct(int productid)
        {
            var conn = _dbService.GetConnection();
            bool isDeleted = false;

            try
            {
                string deleteProduct = @"DELETE FROM product WHERE productid = @productid";

                using (var cmd = new NpgsqlCommand(deleteProduct, conn))
                {
                    cmd.Parameters.AddWithValue("productid", productid);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        isDeleted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saat menghapus produk: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }

            return isDeleted;
        }

        public List<Category> GetCategory()
        {
            List<Category> categories = new List<Category>();
            var conn = _dbService.GetConnection();

            try
            {
                string getCategory = "SELECT categoryid, name, description FROM category";
                using (var cmd = new NpgsqlCommand(getCategory, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                CategoryID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                            });
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

            return categories;
        }

        public void LoadCategory()
        {
            var categories = GetCategory();
            Categories = new ObservableCollection<Category>(categories);
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
        private byte[] ImageToByteArray(BitmapImage image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
