using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarangKu.Models
{
    public class SellerModel : UserModel
    {
        public int SellerId { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public double Rating { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
