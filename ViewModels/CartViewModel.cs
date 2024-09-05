using BarangKu.Models;
using BarangKu.Services;
using System.Collections.Generic;
using System.Windows.Input;

namespace BarangKu.ViewModels
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        private readonly CartService _cartService;

        public ICommand AddToCartCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand UpdateCartItemCommand { get; }
        public ICommand ClearCartCommand { get; }

        public CartViewModel(Cart cart)
        {
            Cart = cart;
            _cartService = new CartService();
            AddToCartCommand = new RelayCommand(AddToCart);
            RemoveFromCartCommand = new RelayCommand(RemoveFromCart);
            UpdateCartItemCommand = new RelayCommand(UpdateCartItem);
            ClearCartCommand = new RelayCommand(ClearCart);
        }

        private void AddToCart()
        {
            _cartService.AddToCart(Cart);
        }

        private void RemoveFromCart()
        {
            _cartService.RemoveFromCart(Cart.UserID, Cart.ProductID);
        }

        private void UpdateCartItem()
        {
            _cartService.UpdateCartItem(Cart.UserID, Cart.ProductID, Cart.Quantity);
        }

        private void ClearCart()
        {
            _cartService.ClearCart(Cart.UserID);
        }
    }
}
