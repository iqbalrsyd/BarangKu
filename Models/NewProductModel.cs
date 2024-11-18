using BarangKu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BarangKu.Models
{
    public class NewProductModel: StoreViewModel
    {
<<<<<<< HEAD
        public override Product AddProduct(int categoryId, string name, string description, decimal price, int stock, string duration, byte[] imageUrl)
        {
            duration = "0"; 
            return base.AddProduct(categoryId, name, description, price, stock, duration, imageUrl);
=======
        public override Product AddProduct(int categoryId, string name, string description, decimal price, int stock, string condition, string duration, byte[] imageUrl)
        {
            duration = "Produk baru";
            return base.AddProduct(categoryId, name, description, price, stock, condition, duration, imageUrl);
>>>>>>> 628ce9e24c95c52ab1179eedd65ed27b017524ee
        }
    }
}
