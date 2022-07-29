using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
	public interface IGenreRepository
	{
		Task<List<Genre>> GetAllGenres();
	}
}

