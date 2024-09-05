using BarangKu.Models;
using System;

namespace BarangKu.Services
{
    public class TransactionService
    {
        public void CreateTransaction(Transaction transaction)
        {
            // Logic to save the transaction to database
            Console.WriteLine("Transaction created with ID: " + transaction.TransactionID);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            // Logic to update transaction details in database
            Console.WriteLine("Transaction updated with ID: " + transaction.TransactionID);
        }

        public void CancelTransaction(Transaction transaction)
        {
            transaction.CancelTransaction();
            // Logic to save the updated status in database
            Console.WriteLine("Transaction cancelled with ID: " + transaction.TransactionID);
        }

        public void RequestReturn(Transaction transaction)
        {
            transaction.RequestReturn();
            // Logic to handle return request in database
            Console.WriteLine("Return requested for transaction ID: " + transaction.TransactionID);
        }
    }
}
