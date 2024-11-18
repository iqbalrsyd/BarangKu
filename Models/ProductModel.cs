using System;
using System.Windows.Media.Imaging;

namespace BarangKu.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int SellerID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
<<<<<<< HEAD
=======

        public string Condition {  get; set; }
>>>>>>> 628ce9e24c95c52ab1179eedd65ed27b017524ee
        public string Duration { get; set; } // e.g., New, Used
        public BitmapImage ImageURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSelected { get; set; }
<<<<<<< HEAD
        public string Condition { get; set; }
        public string Label { get; set; }
=======
        public string Status { get; set; }
        public string CategoryName { get; set; }
>>>>>>> 628ce9e24c95c52ab1179eedd65ed27b017524ee

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

        internal Product AddProduct(int categoryId, string name, string description, decimal price, int stock, string duration, byte[]? imageBytes)
        {
            throw new NotImplementedException();
        }
    }
}
