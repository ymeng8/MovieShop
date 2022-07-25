using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
	public interface IMovieRepository
	{
		Task<List<Movie>> GetTop30HighestRevenueMovies();
		Task<Movie> GetById(int id);
	}
}

