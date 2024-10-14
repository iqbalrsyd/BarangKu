using System;

namespace BarangKu.Models
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
    }
}
