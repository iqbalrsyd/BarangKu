using BarangKu.Models;
using System;

namespace BarangKu.Services
{
    public class OrderService
    {
        public void TrackOrder(Order order)
        {
            // Logic to track the order status
            Console.WriteLine($"Tracking Order {order.OrderId}. Current status: {order.Status}");
        }

        public void RequestReturn(Order order)
        {
            if (order.Status == "Delivered")
            {
                order.Status = "Return Requested";
                // Logic to handle return request
                Console.WriteLine($"Return requested for Order {order.OrderId}.");
            }
            else
            {
                Console.WriteLine($"Order {order.OrderId} cannot be returned. Status: {order.Status}");
            }
        }

        public void CancelOrder(Order order)
        {
            if (order.Status == "Pending")
            {
                order.Status = "Cancelled";
                // Logic to handle order cancellation
                Console.WriteLine($"Order {order.OrderId} has been cancelled.");
            }
            else
            {
                Console.WriteLine($"Order {order.OrderId} cannot be cancelled. Status: {order.Status}");
            }
        }
    }
}
