using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class ReviewRepository : IReviewRepository
	{
        private readonly MovieShopDbContext _movieShopDbContext;
        public ReviewRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Review> AddReview(Review review)
        {
            _movieShopDbContext.Reviews.Add(review);
            await _movieShopDbContext.SaveChangesAsync();
            return review;
        }

        public async Task<Review> EditReview(Review review)
        {
            var oldReview = await GetById(review.MovieId, review.UserId);
            oldReview.Rating = review.Rating;
            oldReview.ReviewText = review.ReviewText;
            oldReview.CreatedDate = review.CreatedDate;
            await _movieShopDbContext.SaveChangesAsync();
            return oldReview;
        }

        public async Task<Review> GetById(int movieId, int userId)
        {
            var review = await _movieShopDbContext.Reviews.Include(r => r.Movie).FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
            return review;
        }

        public async Task<Review> RemoveReview(Review review)
        {
            _movieShopDbContext.Reviews.Remove(review);
            await _movieShopDbContext.SaveChangesAsync();
            return review;
        }
    }
}

