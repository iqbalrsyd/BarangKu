using System;

namespace BarangKu.Models
{
    public class Cart
    {
        public int CartID { get; private set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public Cart(int cartID, int userID, int productID, int quantity)
        {
            CartID = cartID;
            UserID = userID;
            ProductID = productID;
            Quantity = quantity;
        }

        public Cart()
        {
            // Default constructor
        }
    }
}
