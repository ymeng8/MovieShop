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
	}
}

