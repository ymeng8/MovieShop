using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;
        public PurchaseRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Purchase> AddPurchase(Purchase purchase)
        {
            _movieShopDbContext.Purchases.Add(purchase);
            await _movieShopDbContext.SaveChangesAsync();
            return purchase;
        }

        public async Task<Purchase> GetById(int movieId, int userId)
        {
            var purchase = await _movieShopDbContext.Purchases.Where(p => p.UserId == userId && p.MovieId == movieId).Include(p => p.Movie).FirstOrDefaultAsync();
            return purchase;
        }
    }
}

