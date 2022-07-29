using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Service
{
	public interface IGenreService
	{
		Task<List<GenreModel>> GetAllGenres();
	}
}

