using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Service
{
	public interface IMovieService
	{
		Task<List<MovieCardModel>> GetTopRevenueMovies();
	}
}

