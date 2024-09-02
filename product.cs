using System;
using System.Collections.Generic;
using System.Linq;


namespace BarangKu
{
    public class Product
    {
        public int ProductID { get; private set; }
        public int SellerID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Condition { get; set; } // e.g., New, Used
        public string ImageURL { get; set; }
        public DateTime CreatedDate { get; private set; }


        public Product()
        {
            CreatedDate = DateTime.Now;
        }


        public void AddProduct()
        {
            // Logic to add product
        }


        public void EditProduct()
        {
            // Logic to edit product
        }


        public void DeleteProduct()
        {
            // Logic to delete product
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