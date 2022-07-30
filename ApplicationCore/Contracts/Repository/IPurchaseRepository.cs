using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
	public interface IPurchaseRepository
	{
		Task<Purchase> AddPurchase(Purchase purchase);
		Task<Purchase> GetById(int movieId, int userId);
	}
}

