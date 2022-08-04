using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repository
{
	public interface IMovieRepository
	{
		Task<List<Movie>> GetTop30HighestRevenueMovies();
		Task<Movie> GetById(int id);
		Task<PagedResultSet<Movie>> GetMoviesByTitlePaged(string title, int pageSize, int page);
		Task<PagedResultSet<Movie>> GetMoviesByGenrePaged(int genreId, int pageSize, int page);
		Task<PagedResultSet<Review>> GetReviewsOfMovie(int movieId, int pageSize, int page);
		Task<List<Movie>> GetTop30RatedMovies();
	}
}

