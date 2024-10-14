using System;

namespace BarangKu.Models
{
    public class Order
    {
        public int OrderId { get; private set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; private set; }
        public string Status { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
        }
    }
}
