using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Service;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;
        public UserService(IPurchaseRepository purchaseRepository, IUserRepository userRepository, IFavoriteRepository favoriteRepository, IReviewRepository reviewRepository)
        {
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> AddMovieReview(ReviewRequestModel reviewRequest)
        {
            Review dbReview = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText,
                CreatedDate = reviewRequest.CreatedDate
            };
            await _reviewRepository.AddReview(dbReview);
            return true;
        }

        public async Task<bool> UpdateMovieReview(ReviewRequestModel editRequest)
        {
            Review newReview = new Review
            {
                MovieId = editRequest.MovieId,
                UserId = editRequest.UserId,
                Rating = editRequest.Rating,
                ReviewText = editRequest.ReviewText,
                CreatedDate = editRequest.CreatedDate
            };
            await _reviewRepository.EditReview(newReview);
            return true;
        }

        public async Task<bool> DeleteMovieReview(ReviewRequestModel deleteRequest)
        {
            var review = await _reviewRepository.GetById(deleteRequest.MovieId, deleteRequest.UserId);
            await _reviewRepository.RemoveReview(review);
            return true;
        }
        public async Task<bool> ReviewExists(int userId, int movieId)
        {
            var review = await _reviewRepository.GetById(movieId, userId);
            if (review != null)
            {
                return true;
            }
            return false;
        }

        public async Task<ReviewDetailsModel> GetReviewDetails(int userId, int movieId)
        {
            var review = await _reviewRepository.GetById(movieId, userId);
            var reviewDetails = new ReviewDetailsModel
            {
                UserId = userId,
                MovieId = movieId,
                MovieTitle = review.Movie.Title,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                CreatedDate = review.CreatedDate
            };
            return reviewDetails;
        }

        public async Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            Favorite dbFavorite = new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            };
            await _favoriteRepository.AddFavorite(dbFavorite);
            return true;
        }

        public async Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = await _favoriteRepository.GetById(favoriteRequest.MovieId, favoriteRequest.UserId);
            await _favoriteRepository.RemoveFavorite(favorite);
            return true;
        }

        public async Task<bool> FavoriteExists(int userId, int movieId)
        {
            var favorite = await _favoriteRepository.GetById(movieId, userId);
            if (favorite != null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<MovieCardModel>> GetAllFavoritesForUser(int userId)
        {
            var movieCards = new List<MovieCardModel>();
            var userFavorites = await _userRepository.GetUserFavorites(userId);
            foreach (var f in userFavorites.Favorites)
            {
                movieCards.Add(new MovieCardModel { Id = f.MovieId, Title = f.Movie.Title, PosterUrl = f.Movie.PosterUrl });
            }
            return movieCards;
        }

        public async Task<List<MovieCardModel>> GetAllPurchasesForUser(int id)
        {
            var movieCards = new List<MovieCardModel>();

            var userPurchases = await _userRepository.GetUserPurchases(id);
            foreach (var p in userPurchases.Purchases)
            {
                movieCards.Add(new MovieCardModel { Id = p.MovieId, Title = p.Movie.Title, PosterUrl = p.Movie.PosterUrl });
            }

            return movieCards;
        }

        public async Task<PurchaseDetailsModel> GetPurchasesDetails(int userId, int movieId)
        {
            var purchase = await _purchaseRepository.GetById(movieId, userId);
            var purchaseDetails = new PurchaseDetailsModel
            {
                MovieId = purchase.MovieId,
                PosterUrl = purchase.Movie.PosterUrl,
                PurchaseDatetTime = purchase.PurchaseDatetTime,
                PurchaseNumber = purchase.PurchaseNumber,
                Title = purchase.Movie.Title,
                TotalPrice = purchase.TotalPrice,
                UserId = purchase.UserId
            };
            return purchaseDetails;
        }

        public async Task<bool> IsMoviePurchased(int movieId, int userId)
        {
            var purchase = await _purchaseRepository.GetById(movieId, userId);
            if (purchase != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            Purchase dbPurchase = new Purchase
            {
                UserId = purchaseRequest.UserId,
                MovieId = purchaseRequest.MovieId,
                TotalPrice = purchaseRequest.TotalPrice,
                PurchaseNumber = purchaseRequest.PurchaseNumber,
                PurchaseDatetTime = purchaseRequest.PurchaseDateTime
            };
            var savedPurchase = await _purchaseRepository.AddPurchase(dbPurchase);
            if (savedPurchase.PurchaseNumber != Guid.Empty)
            {
                return true;
            }
            return false;
        }
    }
}

