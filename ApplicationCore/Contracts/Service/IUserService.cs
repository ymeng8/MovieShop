using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Service
{
	public interface IUserService
	{
		Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest);
		Task<bool> IsMoviePurchased(int movieId, int userId);
		Task<List<MovieCardModel>> GetAllPurchasesForUser(int id);
		Task<PurchaseDetailsModel> GetPurchasesDetails(int userId, int movieId);

		Task<List<MovieCardModel>> GetAllFavoritesForUser(int userId);
		Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest);
		Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest);
		Task<bool> FavoriteExists(int userId, int movieId);
	}
}

