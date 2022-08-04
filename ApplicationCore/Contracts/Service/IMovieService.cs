using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Service
{
	public interface IMovieService
	{
		Task<List<MovieCardModel>> GetTopRevenueMovies();
		Task<MovieDetailsModel> GetMovieDetails(int movieId);
		Task<PagedResultSet<MovieCardModel>> GetMoviesByTitlePaged(string title, int pageSize, int page);
		Task<PagedResultSet<MovieCardModel>> GetMoviesByGenrePaged(int genreId, int pageSize, int page);
		Task<PagedResultSet<ReviewDetailsModel>> GetReviewsOfMoviePaged(int movieId, int pageSize, int page);
		Task<List<MovieCardModel>> GetTopRatedMovies();
	}
}

