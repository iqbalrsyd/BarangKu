using BarangKu.Models;
using System;

namespace BarangKu.Services
{
    public class ReviewService
    {
        public void AddReview(Review review, Product product, int rating, string reviewText)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

            review.ProductID = product.ProductID;
            review.Rating = rating;
            review.ReviewText = reviewText;
            review.ReviewDate = DateTime.Now;

            // Logic to save the review to the database
            Console.WriteLine($"Review added for Product {product.Name} with rating {rating}");
        }

        public void EditReview(Review review, int newRating, string newReviewText)
        {
            if (newRating < 1 || newRating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

            review.Rating = newRating;
            review.ReviewText = newReviewText;
            review.ReviewDate = DateTime.Now;

            // Logic to update review in the database
            Console.WriteLine($"Review {review.ReviewID} updated with new rating {newRating}");
        }

        public void DeleteReview(Review review)
        {
            // Logic to delete review from the database
            Console.WriteLine($"Review {review.ReviewID} deleted.");
        }
    }
}
