using System;

namespace BarangKu.Models
{
    public class Payment
    {
        public int PaymentID { get; private set; }
        public int TransactionID { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }

        public Payment()
        {
            PaymentDate = DateTime.Now;
        }
    }
}
