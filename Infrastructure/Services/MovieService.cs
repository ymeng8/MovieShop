using System;
using ApplicationCore.Contracts.Service;
using ApplicationCore.Models;
using ApplicationCore.Contracts.Repository;

namespace Infrastructure.Services
{
	public class MovieService : IMovieService
	{
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
		{
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieCardModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.GetTop30HighestRevenueMovies();
            var movieCards = new List<MovieCardModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }
            return movieCards;
        }
    }
}

