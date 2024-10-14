using BarangKu.Models;
using System;

namespace BarangKu.Services
{
    public class ProductService
    {
        public void AddProduct(Product product)
        {
            Console.WriteLine($"Product {product.Name} has been added.");
        }

        public void EditProduct(Product product)
        {
            Console.WriteLine($"Product {product.ProductID} has been updated.");
        }

        public void DeleteProduct(Product product)
        {
            // Logic to delete product from the database
            Console.WriteLine($"Product {product.ProductID} has been deleted.");
        }
    }
}
