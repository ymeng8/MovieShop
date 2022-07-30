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
        public UserService(IPurchaseRepository purchaseRepository, IUserRepository userRepository)
        {
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
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

