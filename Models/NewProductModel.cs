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
        public override Product AddProduct(int categoryId, string name, string description, decimal price, int stock, string condition, string duration, byte[] imageUrl)
        {
            duration = "Produk baru";
            return base.AddProduct(categoryId, name, description, price, stock, condition, duration, imageUrl);
        }
    }
}
