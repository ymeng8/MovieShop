using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repository
{
	public interface IMovieRepository
	{
		Task<List<Movie>> GetTop30HighestRevenueMovies();
		Task<Movie> GetById(int id);
		Task<PagedResultSet<Movie>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int page = 1);
	}
}

