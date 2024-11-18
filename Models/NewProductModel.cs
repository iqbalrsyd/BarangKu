using BarangKu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarangKu.Models
{
    public class NewProductModel: ProductViewModel
    {
        public override Product AddProduct(int categoryId, string name, string description, decimal price, int stock, string duration, byte[] imageUrl)
        {
            duration = "0"; 
            return base.AddProduct(categoryId, name, description, price, stock, duration, imageUrl);
        }
    }
}
