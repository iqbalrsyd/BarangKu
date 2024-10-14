using BarangKu.Models;
using System;
using System.Collections.Generic;

namespace BarangKu.Services
{
    public class MessageService
    {
        private readonly List<Message> _messages; // Simulasi database

        public MessageService()
        {
            // Simulasi daftar pesan, ini bisa diambil dari database pada aplikasi nyata
            _messages = new List<Message>
            {
                new Message(1, 2, "Hello, how are you?"),
                new Message(2, 1, "I'm good, thank you!"),
                new Message(1, 3, "Are you available tomorrow?"),
            };
        }

        // Method untuk mengirim pesan
        public void SendMessage(int senderID, int receiverID, string content)
        {
            var message = new Message(senderID, receiverID, content);
            _messages.Add(message);
            Console.WriteLine("Message sent.");
        }

        // Method untuk mendapatkan semua pesan untuk user tertentu
        public List<Message> GetMessagesForUser(int userID)
        {
            return _messages.FindAll(m => m.ReceiverID == userID); // Cari pesan berdasarkan ReceiverID
        }

        // Method untuk menandai pesan sebagai dibaca
        public void MarkMessageAsRead(int messageID)
        {
            var message = _messages.Find(m => m.MessageID == messageID);
            if (message != null)
            {
                message.MarkAsRead();
                Console.WriteLine("Message marked as read.");
            }
            else
            {
                Console.WriteLine("Message not found.");
            }
        }
    }
}
