using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
	public interface IUserRepository
	{
		Task<User> GetUserById(int userId);
		Task<User> GetUserByEamil(string email);
		Task<User> AddUser(User user);

		Task<User> GetUserPurchases(int userId);
		Task<User> GetUserFavorites(int userId);
		Task<User> GetUserReviews(int userId);
	}
}

