using BarangKu.Models;

namespace BarangKu.ViewModels
{
    public class PaymentViewModel
    {
        public Payment Payment { get; set; }
        public string PaymentSummary => $"Transaction: {Payment.TransactionID}, Amount: {Payment.Amount}, Status: {Payment.PaymentStatus}";

        public PaymentViewModel(Payment payment)
        {
            Payment = payment;
        }
    }
}
