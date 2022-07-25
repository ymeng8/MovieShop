using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class CastRepository : ICastRepository
	{
        private readonly MovieShopDbContext _movieShopDbContext;
        public CastRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Cast> GetById(int id)
        {
            var castDetails = await _movieShopDbContext.Casts
                .Include(c => c.MoviesOfCast).ThenInclude(c => c.Movie)
                .FirstOrDefaultAsync(c => c.Id == id);
            return castDetails;
        }
    }
}

