using BarangKu.Models;
using BarangKu.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BarangKu.ViewModels
{
    public class MessageViewModel
    {
        public ObservableCollection<Message> Messages { get; set; }
        private readonly MessageService _messageService;

        public ICommand SendMessageCommand { get; }
        public ICommand LoadMessagesCommand { get; }
        public ICommand MarkAsReadCommand { get; }

        public MessageViewModel()
        {
            _messageService = new MessageService();
            Messages = new ObservableCollection<Message>();

            SendMessageCommand = new RelayCommand(SendMessage);
            LoadMessagesCommand = new RelayCommand(LoadMessages);
            MarkAsReadCommand = new RelayCommand<int>(MarkAsRead);
        }

        // Method untuk mengirim pesan
        private void SendMessage()
        {
            _messageService.SendMessage(1, 2, "Hello, how are you?");
            LoadMessages(); // Refresh daftar pesan
        }

        // Method untuk memuat semua pesan
        private void LoadMessages()
        {
            Messages.Clear();
            var messages = _messageService.GetMessagesForUser(1); // Panggil GetMessagesForUser dari MessageService
            foreach (var message in messages)
            {
                Messages.Add(message); // Tambahkan ke ObservableCollection untuk binding di UI
            }
        }

        // Method untuk menandai pesan sebagai dibaca
        private void MarkAsRead(int messageId)
        {
            _messageService.MarkMessageAsRead(messageId);
            LoadMessages(); // Refresh daftar pesan setelah pesan ditandai
        }
    }
}
