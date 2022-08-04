using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;
        public MovieRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Movie> GetById(int id)
        {
            var movieDetails = await _movieShopDbContext.Movies
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
                .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
                .Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == id);
            return movieDetails;
        }

        public async Task<List<Movie>> GetTop30HighestRevenueMovies()
        {
            var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<PagedResultSet<Movie>> GetMoviesByTitlePaged(string title, int pageSize, int page)
        {
            var count = await _movieShopDbContext.Movies.Where(m => m.Title.Contains(title)).CountAsync();
            var movies = await _movieShopDbContext.Movies.Where(m => m.Title.Contains(title))
                .OrderByDescending(m => m.Revenue).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedMovies = new PagedResultSet<Movie>(movies, page, pageSize, count);

            return pagedMovies;
        }

        public async Task<PagedResultSet<Movie>> GetMoviesByGenrePaged(int genreId, int pageSize, int page)
        {
            var totalMoviesCountOfGenre = await _movieShopDbContext.MovieGenres.Where(g => g.GenreId == genreId).CountAsync();
            var movies = await _movieShopDbContext.MovieGenres.Where(g => g.GenreId == genreId)
                .Include(g => g.Movie).OrderByDescending(g => g.Movie.Revenue)
                .Select(m => new Movie
                {
                    Id = m.MovieId,
                    PosterUrl = m.Movie.PosterUrl,
                    Title = m.Movie.Title
                })
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedMovies = new PagedResultSet<Movie>(movies, page, pageSize, totalMoviesCountOfGenre);
            return pagedMovies;
        }

        public async Task<PagedResultSet<Review>> GetReviewsOfMovie(int movieId, int pageSize, int page)
        {
            var totalCount = await _movieShopDbContext.Reviews.Where(r => r.MovieId == movieId).CountAsync();
            var reviews = await _movieShopDbContext.Reviews.Where(r => r.MovieId == movieId)
                .Include(r => r.Movie).OrderByDescending(r => r.CreatedDate)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedReviews = new PagedResultSet<Review>(reviews, page, pageSize, totalCount);

            return pagedReviews;
        }

        public async Task<List<Movie>> GetTop30RatedMovies()
        {
            var movies = await _movieShopDbContext.Movies.Include(m => m.Reviews)
                .Select(m => new Movie
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl,
                    Rating = m.Reviews.Average(x => x.Rating)
                }).Where(m => m.Rating != null).OrderByDescending(m => m.Rating).Take(30).ToListAsync();

            return movies;
        }
    }
}

