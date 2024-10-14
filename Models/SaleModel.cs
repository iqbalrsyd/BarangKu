using System;

namespace BarangKu.Models
{
    public class Sale
    {
        public int SaleId { get; private set; }
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; private set; }
        public decimal TotalAmount { get; set; }

        public Sale()
        {
            SaleDate = DateTime.Now;
        }

        public void RecordSale(Product product, int quantity, decimal totalAmount)
        {
            ProductId = product.ProductID;
            Quantity = quantity;
            TotalAmount = totalAmount;
            SaleDate = DateTime.Now;
            // Logic to record sale
        }

        public void TrackShipment(Sale sale)
        {
            // Logic to track shipment
        }
    }
}
