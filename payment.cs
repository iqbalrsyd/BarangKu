using System;

namespace BarangKu
{
    public class Payment
    {
        public int PaymentID { get; private set; }
        public int TransactionID { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; private set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }


        public Payment()
        {
            PaymentDate = DateTime.Now;
        }


        public void ProcessPayment()
        {
            PaymentStatus = "Processed";
            // Logic to process payment
        }


        public void RefundPayment()
        {
            PaymentStatus = "Refunded";
            // Logic to refund payment
        }
    }
}