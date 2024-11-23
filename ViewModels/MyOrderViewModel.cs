using BarangKu.Models;
using BarangKu.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BarangKu.ViewModels
{
    public class MyOrderViewModel
    {
        private readonly DatabaseService _dbService;
        private Authenticator authenticator = new Authenticator();
        public ObservableCollection<Transaction> Orders { get; set; }
        public ObservableCollection<Transaction> FinishedOrders { get; set; }
        public MyOrderViewModel()
        {
            _dbService = new DatabaseService();
            LoadOrder();
        }


        public List<Transaction> GetOrder()
        {
            int userid = UserSessionService.Instance.User.UserId;
            List<Transaction> orders = new List<Transaction>();
            var conn = _dbService.GetConnection();

            if(authenticator.IsUserSeller(userid))
            {
                try
                {
                    int sellerid = UserSessionService.Instance.Seller.SellerId;
                    string getOrder = @"
                            SELECT 
                                t.ordertransactionid,
                                t.buyerid,
                                t.sellerid,
                                t.productid,
                                t.quantity,
                                t.status,
                                t.totalamount,
                                t.paymentstatus,
                                u.username,
                                u.address,
                                u.telephone,
                                p.name,
                                p.imageurl,
                                t.ShippingMethod
                            FROM 
                                order_transaction t
                            JOIN 
                                product p ON t.productid = p.productid
                            JOIN 
                                users u ON t.buyerid = u.userid
                            WHERE 
                                t.sellerid = @sellerid AND t.status = 'Diproses'";


                    using (var cmd = new NpgsqlCommand(getOrder, conn))
                    {
                        cmd.Parameters.AddWithValue("sellerid", sellerid);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
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
                                    BuyerName = reader.GetString(8),
                                    BuyerAddress = reader.GetString(9),
                                    BuyerTelephone = reader.GetString(10),
                                    ProductName = reader.GetString(11),
                                    ImageURL = reader.IsDBNull(12) ? null : ByteArrayToImage((byte[])reader[12]),
                                    ShippingMethod = reader.GetString(13),
                                };

                                orders.Add(order);
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

            return orders;
        }

        public List<Transaction> GetOrderFinished()
        {
            int userid = UserSessionService.Instance.User.UserId;
            List<Transaction> orders = new List<Transaction>();
            var conn = _dbService.GetConnection();

            if (authenticator.IsUserSeller(userid))
            {
                try
                {
                    int sellerid = UserSessionService.Instance.Seller.SellerId;
                    string getOrder = @"
                            SELECT 
                                t.ordertransactionid,
                                t.buyerid,
                                t.sellerid,
                                t.productid,
                                t.quantity,
                                t.status,
                                t.totalamount,
                                t.paymentstatus,
                                u.username,
                                u.address,
                                u.telephone,
                                p.name,
                                p.imageurl,
                                t.ShippingMethod
                            FROM 
                                order_transaction t
                            JOIN 
                                product p ON t.productid = p.productid
                            JOIN 
                                users u ON t.buyerid = u.userid
                            WHERE 
                                t.sellerid = @sellerid AND t.status = 'Selesai'";


                    using (var cmd = new NpgsqlCommand(getOrder, conn))
                    {
                        cmd.Parameters.AddWithValue("sellerid", sellerid);
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
                                    BuyerName = reader.GetString(8),
                                    BuyerAddress = reader.GetString(9),
                                    BuyerTelephone = reader.GetString(10),
                                    ProductName = reader.GetString(11),
                                    ImageURL = reader.IsDBNull(12) ? null : ByteArrayToImage((byte[])reader[12]),
                                    ShippingMethod = reader.GetString(13),
                                };

                                orders.Add(order);
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

            return orders;
        }

        public void LoadOrder()
        {
            var orderList = GetOrder();
            Orders = new ObservableCollection<Transaction>(orderList);
            var orderFinishedList = GetOrderFinished();
            FinishedOrders = new ObservableCollection<Transaction>(orderFinishedList);
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
