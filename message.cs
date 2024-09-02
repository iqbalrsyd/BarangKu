using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarangKu
{
    public class message
    {
        public int MessageID { get; private set; }
        public int SenderID { get; private set; }
        public int ReceiverID { get; private set; }
        public string Content { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Boolean IsRead   { get; private set; }

        public void SendMessage(User user, string content)
        {
            SenderID = user.UserId;
            Content = content;
        }

        public void ReceiveMessage(User user, string content)
        {
            SenderID = user.UserId;
            Content = content;
        }

    }
}
