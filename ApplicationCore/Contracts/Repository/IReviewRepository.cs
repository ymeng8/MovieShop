using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
	public interface IReviewRepository
	{
		Task<Review> AddReview(Review review);
		Task<Review> EditReview(Review review);
		Task<Review> RemoveReview(Review review);
		Task<Review> GetById(int movieId, int userId);
	}
}

