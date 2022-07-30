using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;
        public FavoriteRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
            _movieShopDbContext.Favorites.Add(favorite);
            await _movieShopDbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task<Favorite> GetById(int movieId, int userId)
        {
            var favorite = await _movieShopDbContext.Favorites.Include(f => f.Movie).FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId);
            return favorite;
        }

        public async Task<Favorite> RemoveFavorite(Favorite favorite)
        {
            _movieShopDbContext.Favorites.Remove(favorite);
            await _movieShopDbContext.SaveChangesAsync();
            return favorite;
        }
    }
}

