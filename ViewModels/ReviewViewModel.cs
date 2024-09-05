using BarangKu.Models;
using System.Collections.Generic;

namespace BarangKu.ViewModels
{
    public class ReviewViewModel
    {
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
        public Review NewReview { get; set; }
    }
}
