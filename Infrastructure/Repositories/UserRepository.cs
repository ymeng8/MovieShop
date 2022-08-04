using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly MovieShopDbContext _movieShopDbContext;
        public UserRepository(MovieShopDbContext movieShopDbContext)
		{
            _movieShopDbContext = movieShopDbContext;
		}

        public async Task<User> AddUser(User user)
        {
            _movieShopDbContext.Users.Add(user);
            await _movieShopDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEamil(string email)
        {
            var user = await _movieShopDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> GetUserPurchases(int userId)
        {
            var purchases = await _movieShopDbContext.Users.Include(u => u.Purchases).ThenInclude(p => p.Movie).FirstOrDefaultAsync(u => u.Id == userId);
            return purchases;
        }

        public async Task<User> GetUserFavorites(int userId)
        {
            var favorites = await _movieShopDbContext.Users.Include(u => u.Favorites).ThenInclude(f => f.Movie).FirstOrDefaultAsync(u => u.Id == userId);
            return favorites;
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await _movieShopDbContext.Users.Include(u => u.RolesOfUser).FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<User> GetUserReviews(int userId)
        {
            var reviews = await _movieShopDbContext.Users.Include(u => u.Reviews).ThenInclude(r => r.Movie).FirstOrDefaultAsync(u => u.Id == userId);
            return reviews;
        }
    }
}

