﻿using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public GenreRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            var genres = await _movieShopDbContext.Genres.ToListAsync();
            return genres;
        }
    }
}

