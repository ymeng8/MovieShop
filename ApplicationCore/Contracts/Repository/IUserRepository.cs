using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
	public interface IUserRepository
	{
		Task<User> GetUserByEamil(string email);
		Task<User> AddUser(User user);

		Task<User> GetUserPurchases(int userId);

		Task<User> GetUserFavorites(int userId);
	}
}

