using System;

namespace BarangKu.Models
{
    public class Review
    {
        public int ReviewID { get; private set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public int Rating { get; set; } // Rating between 1 and 5
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public Review()
        {
            ReviewDate = DateTime.Now;
        }
    }
}