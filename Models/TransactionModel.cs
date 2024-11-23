using BarangKu.Models;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BarangKu.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int BuyerID { get; set; }
        public int SellerID { get; set; }
        public int ProductID { get; set; }
        public DateTime TransactionDate { get; private set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public int Quantity { get; set; }
        public string PaymentStatus { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerTelephone { get; set; }
        public string ShippingMethod { get; set; }
        public string ProductName { get; set; }
        public BitmapImage ImageURL { get; set; }
        public Transaction()
        {
            TransactionDate = DateTime.Now;
        }

        public void CreateTransaction()
        {
            Status = "Created";
            // Logic to create a new transaction
        }

        public void CancelTransaction()
        {
            Status = "Cancelled";
            // Logic to cancel the transaction
        }

        public void UpdateTransaction()
        {
            // Logic to update the transaction details
        }

        public void RequestReturn()
        {
            Status = "Return Requested";
            // Logic to request a return
        }

        private string _activeButton = "Ordered"; // Default aktif "Dipesan"
        public string ActiveButton
        {
            get => _activeButton;
            set { _activeButton = value; OnPropertyChanged(); }
        }

        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }
    }

}
