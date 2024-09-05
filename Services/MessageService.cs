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
            _messages = new List<Message>();
        }

        public void SendMessage(int senderID, int receiverID, string content)
        {
            var message = new Message(senderID, receiverID, content);
            _messages.Add(message);
            Console.WriteLine("Message sent.");
        }

        public List<Message> GetMessagesForUser(int userID)
        {
            return _messages.FindAll(m => m.ReceiverID == userID);
        }

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
    