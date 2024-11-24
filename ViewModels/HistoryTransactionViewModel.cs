using BarangKu.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarangKu.Models;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;

namespace BarangKu.ViewModels
{
    public class HistoryTransactionViewModel
    {
        private readonly DatabaseService _dbService;
        public ObservableCollection<Transaction> BookedOrders { get; set; }
        public ObservableCollection<Transaction> SentOrders { get; set; }
        public ObservableCollection<Transaction> FinishedOrders { get; set; }

        public HistoryTransactionViewModel()
        {
            _dbService = new DatabaseService();
            LoadTransaction();
        }

        public List<Transaction> GetBookedOrders()
        {
            int userid = UserSessionService.Instance.User.UserId;
            List<Transaction> bookedOrders = new List<Transaction>();
            var conn = _dbService.GetConnection();

            try
            {
                string getOrder = @"SELECT
                                    t.ordertransactionid,
                                    t.buyerid,
                                    t.sellerid,
                                    t.productid,
                                    t.quantity,
                                    t.status,
                                    t.totalamount,
                                    t.paymentstatus,
                                    t.shippingmethod,
                                    t.orderdate,
                                    p.name,
                                    p.imageurl,
                                    s.storename
                                FROM
                                    order_transaction t
                                JOIN
                                    product p ON t.productid = p.productid
                                JOIN
                                    seller s ON t.sellerid = s.sellerid
                                WHERE
                                    t.buyerid = @userid AND t.status = 'Dipesan'";

                using (var cmd = new NpgsqlCommand(getOrder, conn))
                {
                    cmd.Parameters.AddWithValue("userid", userid);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transaction order = new Transaction
                            {
                                TransactionID = reader.GetInt32(0),
                                BuyerID = reader.GetInt32(1),
                                SellerID = reader.GetInt32(2),
                                ProductID = reader.GetInt32(3),
                                Quantity = reader.GetInt32(4),
                                Status = reader.GetString(5),
                                TotalAmount = reader.GetDecimal(6),
                                PaymentStatus = reader.GetString(7),
                                ShippingMethod = reader.GetString(8),
                                TransactionDate = reader.GetDateTime(9),
                                ProductName = reader.GetString(10),
                                ImageURL = reader.IsDBNull(11) ? null : ByteArrayToImage((byte[])reader[11]),
                                StoreName = reader.GetString(12),
                            };
                            bookedOrders.Add(order);
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
            return bookedOrders;
        }

        public List<Transaction> GetSentOrders()
        {
            int userid = UserSessionService.Instance.User.UserId;
            List<Transaction> sentOrders = new List<Transaction>();
            var conn = _dbService.GetConnection();

            try
            {
                string getOrder = @"SELECT
                                    t.ordertransactionid,
                                    t.buyerid,
                                    t.sellerid,
                                    t.productid,
                                    t.quantity,
                                    t.status,
                                    t.totalamount,
                                    t.paymentstatus,
                                    t.shippingmethod,
                                    t.orderdate,
                                    p.name,
                                    p.imageurl,
                                    s.storename
                                FROM
                                    order_transaction t
                                JOIN
                                    product p ON t.productid = p.productid
                                JOIN
                                    seller s ON t.sellerid = s.sellerid
                                WHERE
                                    t.buyerid = @userid AND t.status = 'Dikirim'";

                using (var cmd = new NpgsqlCommand(getOrder, conn))
                {
                    cmd.Parameters.AddWithValue("userid", userid);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transaction order = new Transaction
                            {
                                TransactionID = reader.GetInt32(0),
                                BuyerID = reader.GetInt32(1),
                                SellerID = reader.GetInt32(2),
                                ProductID = reader.GetInt32(3),
                                Quantity = reader.GetInt32(4),
                                Status = reader.GetString(5),
                                TotalAmount = reader.GetDecimal(6),
                                PaymentStatus = reader.GetString(7),
                                ShippingMethod = reader.GetString(8),
                                TransactionDate = reader.GetDateTime(9),
                                ProductName = reader.GetString(10),
                                ImageURL = reader.IsDBNull(11) ? null : ByteArrayToImage((byte[])reader[11]),
                                StoreName = reader.GetString(12),
                            };
                            sentOrders.Add(order);
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
            return sentOrders;
        }

        public List<Transaction> GetFinishedOrders()
        {
            int userid = UserSessionService.Instance.User.UserId;
            List<Transaction> finishedOrders = new List<Transaction>();
            var conn = _dbService.GetConnection();

            try
            {
                string getOrder = @"SELECT
                                    t.ordertransactionid,
                                    t.buyerid,
                                    t.sellerid,
                                    t.productid,
                                    t.quantity,
                                    t.status,
                                    t.totalamount,
                                    t.paymentstatus,
                                    t.shippingmethod,
                                    t.orderdate,
                                    p.name,
                                    p.imageurl,
                                    s.storename
                                FROM
                                    order_transaction t
                                JOIN
                                    product p ON t.productid = p.productid
                                JOIN
                                    seller s ON t.sellerid = s.sellerid
                                WHERE
                                    t.buyerid = @userid AND t.status = 'Selesai'";

                using (var cmd = new NpgsqlCommand(getOrder, conn))
                {
                    cmd.Parameters.AddWithValue("userid", userid);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transaction order = new Transaction
                            {
                                TransactionID = reader.GetInt32(0),
                                BuyerID = reader.GetInt32(1),
                                SellerID = reader.GetInt32(2),
                                ProductID = reader.GetInt32(3),
                                Quantity = reader.GetInt32(4),
                                Status = reader.GetString(5),
                                TotalAmount = reader.GetDecimal(6),
                                PaymentStatus = reader.GetString(7),
                                ShippingMethod = reader.GetString(8),
                                TransactionDate = reader.GetDateTime(9),
                                ProductName = reader.GetString(10),
                                ImageURL = reader.IsDBNull(11) ? null : ByteArrayToImage((byte[])reader[11]),
                                StoreName = reader.GetString(12),
                            };
                            finishedOrders.Add(order);
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
            return finishedOrders;
        }

        private BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        public void LoadTransaction()
        {
            var bookedTransactionList = GetBookedOrders();
            BookedOrders = new ObservableCollection<Transaction>(bookedTransactionList);
            var sentTransactionList = GetSentOrders();
            SentOrders = new ObservableCollection<Transaction>(sentTransactionList);
            var finishedTransactionList = GetFinishedOrders();
            FinishedOrders = new ObservableCollection<Transaction>(finishedTransactionList);
        }

        public bool UpdateStatus(int transactionid)
        {
            var conn = _dbService.GetConnection();
            bool isUpdated = false;

            try
            {
                string updateStatusQuery = @"
            UPDATE order_transaction 
            SET status = 'Selesai' 
            WHERE ordertransactionid = @ordertransactionid";

                using (var cmd = new NpgsqlCommand(updateStatusQuery, conn))
                {
                    cmd.Parameters.AddWithValue("ordertransactionid", transactionid);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        isUpdated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saat mengubah status produk: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }

            return isUpdated;
        }
    }
}
