using BarangKu.Models;
using BarangKu.Services;
using System.ComponentModel;

namespace BarangKu.ViewModels
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        private Transaction _transaction;
        private readonly TransactionService _transactionService;

        public TransactionViewModel()
        {
            _transaction = new Transaction();
            _transactionService = new TransactionService();
        }

        public Transaction Transaction
        {
            get { return _transaction; }
            set
            {
                _transaction = value;
                OnPropertyChanged("Transaction");
            }
        }

        public void CreateTransaction()
        {
            _transactionService.CreateTransaction(_transaction);
            OnPropertyChanged("Transaction");
        }

        public void CancelTransaction()
        {
            _transactionService.CancelTransaction(_transaction);
            OnPropertyChanged("Transaction");
        }

        public void RequestReturn()
        {
            _transactionService.RequestReturn(_transaction);
            OnPropertyChanged("Transaction");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
