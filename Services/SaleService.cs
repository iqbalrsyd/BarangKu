using BarangKu.Models;
using System;

namespace BarangKu.Services
{
    public class SaleService
    {
        public void RecordSale(Sale sale, Product product, int quantity, decimal totalAmount)
        {
            sale.RecordSale(product, quantity, totalAmount);
            // Logic to save sale to database
            Console.WriteLine("Sale recorded: " + sale.SaleId);
        }

        public void TrackShipment(Sale sale)
        {
            // Logic to track shipment
            sale.TrackShipment(sale);
            Console.WriteLine("Shipment tracked for sale: " + sale.SaleId);
        }
    }
}
