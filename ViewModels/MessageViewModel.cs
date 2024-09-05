using BarangKu.Models;
using BarangKu.Services;
using System.Collections.Generic;
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

        private void SendMessage()
        {
            // Example sending a message
            _messageService.SendMessage(1, 2, "Hello, how are you?");
            LoadMessages(); // Refresh list
        }

        private void LoadMessages()
        {
            Messages.Clear();
            var messages = _messageService.GetMessagesByUser(1); // Example user ID
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }

        private void MarkAsRead(int messageId)
        {
            _messageService.MarkMessageAsRead(messageId);
            LoadMessages(); // Refresh list
        }
    }
}
