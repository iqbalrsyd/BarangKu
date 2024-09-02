using System;

namespace BarangKu
{
    public class Review
    {
        public int ReviewID { get; private set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int OrderId { get; set; }
        public int Rating { get; set; } // Rating between 1 and 5
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; private set; }


        public Review()
        {
            ReviewDate = DateTime.Now;
        }


        public void AddReview(Product product, int rating, string reviewText)
        {
            ProductID = product.ProductID;
            Rating = rating;
            ReviewText = reviewText;
            ReviewDate = DateTime.Now;
            // Logic to add review
        }


        public void EditReview(Review review)
        {
            Rating = review.Rating;
            ReviewText = review.ReviewText;
            // Logic to edit review
        }


        public void DeleteReview(Review review)
        {
            // Logic to delete review
        }
    }
}