using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Service
{
	public interface IMovieService
	{
		Task<List<MovieCardModel>> GetTopRevenueMovies();
		Task<MovieDetailsModel> GetMovieDetails(int movieId);
		Task<PagedResultSet<MovieCardModel>> GetMoviesByPagination(int genreId, int pageSize = 30, int page = 1);
	}
}

