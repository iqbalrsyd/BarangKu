using BarangKu.Models;
using BarangKu.Services;
using Npgsql;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BarangKu.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _dbService;
        private readonly CartService _cartService;
        private ObservableCollection<Cart> _cart;
        public ICommand CheckoutCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CartViewModel()
        {
            _cartService = new CartService();
            _dbService = new DatabaseService();
            Cart = new ObservableCollection<Cart>();
            int userid = UserSessionService.Instance.User.UserId;
            LoadCartItems(userid);
        }


        public ObservableCollection<Cart> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                OnPropertyChanged(nameof(Cart));  // Memberitahu UI jika data berubah
            }
        }

        public void LoadCartItems(int userId)
        {
            try
            {
                userId = UserSessionService.Instance.User.UserId;
                var items = GetCartItems(userId);
                Cart = new ObservableCollection<Cart>(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat memuat keranjang: {ex.Message}", "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Cart> GetCartItems(int userId)
        {
            userId = UserSessionService.Instance.User.UserId;
            List<Cart> cartItems = new List<Cart>();
            var conn = _dbService.GetConnection();

            try
            {
                string query = @"
                SELECT 
                    c.cartid, c.userid, c.productid, c.quantity, 
                    p.name, p.price, p.duration, p.imageurl
                FROM 
                    cart c
                INNER JOIN 
                    product p ON c.productid = p.productid
                WHERE 
                    c.userid = @userId";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cart cartItem = new Cart
                            {
                                CartID = reader.GetInt32(0),
                                UserID = reader.GetInt32(1),
                                ProductID = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Name = reader.GetString(4),
                                Price = reader.GetDecimal(5),
                                Duration = reader.GetString(6),
                                ImageURL = reader.IsDBNull(7) ? null : ByteArrayToImage((byte[])reader[7])
                            };

                            cartItems.Add(cartItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat mengambil item keranjang: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }

            return cartItems;
        }

        public virtual Cart AddCart(int userId, int productId, int quantity)
        {
            Cart cart = null;
            var conn = _dbService.GetConnection();

            try
            {
                int cartId;
                bool isUnique = false;

                // Generate unique CartID
                do
                {
                    Random random = new Random();
                    cartId = random.Next(1000, 9999);

                    string checkCartId = "SELECT COUNT(1) FROM cart WHERE cartid = @cartid";
                    using (var checkCmd = new NpgsqlCommand(checkCartId, conn))
                    {
                        checkCmd.Parameters.AddWithValue("cartid", cartId);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        isUnique = (count == 0);
                    }
                } while (!isUnique);

                // Insert new cart entry
                string addCartQuery = @"INSERT INTO cart (cartid, userid, productid, quantity) 
                                VALUES (@cartid, @userId, @productId, @quantity)";

                using (var cmd = new NpgsqlCommand(addCartQuery, conn))
                {
                    cmd.Parameters.AddWithValue("cartid", cartId);
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("productId", productId);
                    cmd.Parameters.AddWithValue("quantity", quantity);

                    cmd.ExecuteNonQuery();

                    cart = new Cart
                    {
                        CartID = cartId,
                        UserID = userId,
                        ProductID = productId,
                        Quantity = quantity
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat menambahkan ke keranjang: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }

            return cart;
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

        //public void CheckoutSelectedItems()
        //{
        //    // Filter produk yang dipilih
        //    var selectedProducts = Cart.Where(c => c.IsSelected).ToList();



        //    foreach (var product in selectedProducts)
        //    {
        //        Checkout(product); // Panggil fungsi Checkout untuk setiap produk
        //    }

        //    MessageBox.Show("Produk berhasil diproses checkout.", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);

        //    // Hapus item yang sudah diproses dari keranjang
        //    foreach (var product in selectedProducts)
        //    {
        //        Cart.Remove(product);
        //    }
        //}

        public virtual bool AddOrderTransaction(int userId, int productId, int quantity)
        {
            var conn = _dbService.GetConnection();
            try
            {
                int orderTransactionId;
                bool isUnique = false;

                // Generate unique OrderTransactionID
                do
                {
                    Random random = new Random();
                    orderTransactionId = random.Next(10000, 99999); // Menghasilkan ID unik dengan 6 digit

                    // Cek apakah orderTransactionId sudah ada di database
                    string checkOrderTransactionIdQuery = "SELECT COUNT(1) FROM order_transaction WHERE ordertransactionid = @orderTransactionId";
                    using (var checkCmd = new NpgsqlCommand(checkOrderTransactionIdQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("orderTransactionId", orderTransactionId);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        isUnique = (count == 0); // Jika count == 0, berarti ID unik
                    }
                } while (!isUnique); // Lanjutkan mencari ID yang unik sampai ditemukan

                // Masukkan data ke tabel order_transaction
                string addOrderTransactionQuery = @"
            INSERT INTO order_transaction (
                ordertransactionid, buyerid, sellerid, productid, quantity, totalamount, orderdate, status, paymentstatus, shippingmethod
            )
            SELECT
                @orderTransactionId,
                @userId,
                p.sellerid,
                @productId,
                @quantity,
                p.price * @quantity,
                NOW(),
                'Dipesan',
                'Dibayar',
                'COD'
            FROM product p
            WHERE p.productid = @productId";

                using (var cmd = new NpgsqlCommand(addOrderTransactionQuery, conn))
                {
                    cmd.Parameters.AddWithValue("orderTransactionId", orderTransactionId);
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("productId", productId);
                    cmd.Parameters.AddWithValue("quantity", quantity);
                    cmd.ExecuteNonQuery();
                }

                // Jika sampai sini, berarti transaksi berhasil
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat membuat order transaction: {ex.Message}", "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; // Transaksi gagal
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }
        }





    }
}
