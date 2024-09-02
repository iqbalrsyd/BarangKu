using System;

namespace BarangKu
{
    // Class Cart
    public class Cart
    {
        public int CartID { get; private set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }


        public void AddToCart(Product product, int quantity)
        {
            ProductID = product.ProductID;
            Quantity = quantity;
            // Logic to add product to the cart
        }


        public void RemoveFromCart(Product product)
        {
            if (ProductID == product.ProductID)
            {
                // Logic to remove product from the cart
            }
        }


        public void UpdateCartItem(Product product, int quantity)
        {
            if (ProductID == product.ProductID)
            {
                Quantity = quantity;
                // Logic to update the quantity of product in the cart
            }
        }


        public void ClearCart()
        {
            // Logic to clear all items from the cart
        }
    }
}