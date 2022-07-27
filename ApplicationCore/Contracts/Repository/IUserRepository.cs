using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
	public interface IUserRepository
	{
		Task<User> GetUserByEamil(string email);
		Task<User> AddUser(User user);
	}
}

