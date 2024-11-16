using BarangKu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarangKu.Models
{
    public class PrelovedProductModel : StoreViewModel
    {
        public override Product AddProduct(int categoryId, string name, string description, decimal price, int stock, string condition, string duration, byte[] imageUrl)
        {
            return base.AddProduct(categoryId, name, description, price, stock, condition, duration, imageUrl);
        }
    }
}
