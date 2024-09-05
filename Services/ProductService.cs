using BarangKu.Models;
using System;

namespace BarangKu.Services
{
    public class ProductService
    {
        public void AddProduct(Product product)
        {
            // Logic to save product to the database
            Console.WriteLine($"Product {product.Name} has been added.");
        }

        public void EditProduct(Product product)
        {
            // Logic to update product details in the database
            Console.WriteLine($"Product {product.ProductID} has been updated.");
        }

        public void DeleteProduct(Product product)
        {
            // Logic to delete product from the database
            Console.WriteLine($"Product {product.ProductID} has been deleted.");
        }

        public double CalculateDistance(Product product, UserAddress buyerAddress)
        {
            return product.CalculateDistance(buyerAddress);
        }


        public double CalculateDistance(UserAddress buyer)
        {
            // Haversine formula to calculate distance between two coordinates
            double R = 6371; // Radius of Earth in kilometers
            double lat1 = Latitude;
            double lon1 = Longitude;
            double lat2 = buyer.Latitude;
            double lon2 = buyer.Longitude;

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;

            return distance;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
