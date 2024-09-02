using System;

namespace BarangKu
{
    // Class Order
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


        public void TrackOrder()
        {
            // Logic to track order
        }


        public void RequestReturn()
        {
            // Logic to request return
        }


        public void CancelOrder()
        {
            // Logic to cancel order
        }
    }
}