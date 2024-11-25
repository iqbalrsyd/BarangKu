using System;
using System.Windows.Media.Imaging;

namespace BarangKu.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public decimal Price {  get; set; }
        public BitmapImage ImageURL { get; set; }
    }
}