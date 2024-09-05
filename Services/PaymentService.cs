using BarangKu.Models;
using System;

namespace BarangKu.Services
{
    public class PaymentService
    {
        public void ProcessPayment(Payment payment)
        {
            payment.PaymentStatus = "Processed";
            payment.PaymentDate = DateTime.Now;

            // Logic to process the payment
            Console.WriteLine($"Payment {payment.PaymentID} for Transaction {payment.TransactionID} has been processed.");
        }

        public void RefundPayment(Payment payment)
        {
            if (payment.PaymentStatus == "Processed")
            {
                payment.PaymentStatus = "Refunded";
                payment.PaymentDate = DateTime.Now;

                // Logic to refund the payment
                Console.WriteLine($"Payment {payment.PaymentID} for Transaction {payment.TransactionID} has been refunded.");
            }
            else
            {
                Console.WriteLine($"Payment {payment.PaymentID} cannot be refunded. Status: {payment.PaymentStatus}");
            }
        }
    }
}
