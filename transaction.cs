using System;

namespace BarangKu
{
    public class Transaction
    {
        public int TransactionID { get; private set; }
        public int BuyerID { get; set; }
        public int SellerID { get; set; }
        public int ProductID { get; set; }
        public DateTime TransactionDate { get; private set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }


        public Transaction()
        {
            TransactionDate = DateTime.Now;
        }


        public void CreateTransaction()
        {
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
    }
}
