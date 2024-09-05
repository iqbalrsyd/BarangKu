using System;

namespace BarangKu.Models
{
    public class Message
    {
        // Atribut atau Properti
        public int MessageID { get; private set; }
        public int SenderID { get; private set; }
        public int ReceiverID { get; private set; }
        public string Content { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool IsRead { get; private set; }

        // Constructor
        public Message(int senderID, int receiverID, string content)
        {
            SenderID = senderID;
            ReceiverID = receiverID;
            Content = content;
            Timestamp = DateTime.Now;
            IsRead = false;
        }

        // Default constructor
        public Message() { }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}
