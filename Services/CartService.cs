using BarangKu.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarangKu.Services
{
    public class CartService
    {
        private readonly List<Cart> _carts; // Simulasi database

        public CartService()
        {
            _carts = new List<Cart>();
        }

        public void AddToCart(Cart cart)
        {
            // Logic to add product to the cart
            var existingCartItem = _carts.FirstOrDefault(c => c.UserID == cart.UserID && c.ProductID == cart.ProductID);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cart.Quantity; // Update quantity if item already exists
            }
            else
            {
                _carts.Add(cart);
            }
            Console.WriteLine($"Product {cart.ProductID} added to cart.");
        }

        public void RemoveFromCart(int userID, int productID)
        {
            // Logic to remove product from the cart
            var cartItem = _carts.FirstOrDefault(c => c.UserID == userID && c.ProductID == productID);
            if (cartItem != null)
            {
                _carts.Remove(cartItem);
                Console.WriteLine($"Product {productID} removed from cart.");
            }
            else
            {
                Console.WriteLine("Product not found in cart.");
            }
        }

        public void UpdateCartItem(int userID, int productID, int quantity)
        {
            // Logic to update the quantity of a product in the cart
            var cartItem = _carts.FirstOrDefault(c => c.UserID == userID && c.ProductID == productID);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                Console.WriteLine($"Product {productID} quantity updated to {quantity}.");
            }
            else
            {
                Console.WriteLine("Product not found in cart.");
            }
        }

        public void ClearCart(int userID)
        {
            // Logic to clear all items from the cart for a user
            _carts.RemoveAll(c => c.UserID == userID);
            Console.WriteLine($"All items cleared from cart for user {userID}.");
        }

        public List<Cart> GetCartItems(int userID)
        {
            // Filter the cart items for the given user
            var userCartItems = _carts.Where(c => c.UserID == userID).ToList();

            if (!userCartItems.Any())
            {
                Console.WriteLine($"No items found in the cart for user {userID}.");
                return new List<Cart>(); // Return an empty list if no items are found
            }

            Console.WriteLine($"Retrieved {userCartItems.Count} items from cart for user {userID}.");
            return userCartItems;
        }

    }
}
