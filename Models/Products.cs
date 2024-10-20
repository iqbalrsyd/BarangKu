using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarangKu.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image {  get; set; }
        public string Location { get; set; }
        public int Duration { get; set; }

        public Products(int productId,string label, string name, double price, string image, string location, int duration)
        {
            ProductId = productId;
            Label = label;
            Name = name;
            Price = price;
            Image = image;
            Location = location;
            Duration = duration;
        }
        
    }

}
