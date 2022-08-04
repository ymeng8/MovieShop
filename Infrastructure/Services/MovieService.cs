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

        public async Task<MovieDetailsModel> GetMovieDetails(int movieId)
        {
            var movieDetails = await _movieRepository.GetById(movieId);

            var movieDetailsModel = new MovieDetailsModel
            {
                Id = movieDetails.Id,
                Title = movieDetails.Title,
                PosterUrl = movieDetails.PosterUrl,
                BackdropUrl = movieDetails.BackdropUrl,
                OriginalLanguage = movieDetails.OriginalLanguage,
                Overview = movieDetails.Overview,
                Budget = movieDetails.Budget,
                ReleaseDate = movieDetails.ReleaseDate,
                Revenue = movieDetails.Revenue,
                ImdbUrl = movieDetails.ImdbUrl,
                TmdbUrl = movieDetails.TmdbUrl,
                RunTime = movieDetails.RunTime,
                Tagline = movieDetails.Tagline,
                Price = movieDetails.Price
            };

            foreach (var trailer in movieDetails.Trailers)
            {
                movieDetailsModel.Trailers.Add(new TrailerModel
                {
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl
                });
            }

            foreach (var cast in movieDetails.CastsOfMovie)
            {
                movieDetailsModel.Casts.Add(new CastModel { Id = cast.CastId, Name = cast.Cast.Name, Character = cast.Character, ProfilePath = cast.Cast.ProfilePath });
            }

            foreach (var genre in movieDetails.GenresOfMovie)
            {
                movieDetailsModel.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }

            return movieDetailsModel;
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

        public async Task<PagedResultSet<MovieCardModel>> GetMoviesByTitlePaged(string title, int pageSize, int page)
        {
            var movies = await _movieRepository.GetMoviesByTitlePaged(title, pageSize, page);
            var movieCards = new List<MovieCardModel>();
            movieCards.AddRange(movies.Data.Select(m => new MovieCardModel
            {
                Id = m.Id,
                PosterUrl = m.PosterUrl,
                Title = m.Title
            }));

            return new PagedResultSet<MovieCardModel>(movieCards, page, pageSize, movies.TotalRowCount);
        }

        public async Task<PagedResultSet<MovieCardModel>> GetMoviesByGenrePaged(int genreId, int pageSize, int page)
        {
            var movies = await _movieRepository.GetMoviesByGenrePaged(genreId, pageSize, page);
            var movieCards = new List<MovieCardModel>();
            movieCards.AddRange(movies.Data.Select(m => new MovieCardModel
            {
                Id = m.Id,
                PosterUrl = m.PosterUrl,
                Title = m.Title
            }));

            return new PagedResultSet<MovieCardModel>(movieCards, page, pageSize, movies.TotalRowCount);
        }

        public async Task<PagedResultSet<ReviewDetailsModel>> GetReviewsOfMoviePaged(int movieId, int pageSize, int page)
        {
            var reviews = await _movieRepository.GetReviewsOfMovie(movieId, pageSize, page);
            var reviewCards = new List<ReviewDetailsModel>();
            reviewCards.AddRange(reviews.Data.Select(r => new ReviewDetailsModel
            {
                MovieId = r.MovieId,
                MovieTitle = r.Movie.Title,
                UserId = r.UserId,
                Rating = r.Rating,
                ReviewText = r.ReviewText,
                CreatedDate = r.CreatedDate
            }));

            return new PagedResultSet<ReviewDetailsModel>(reviewCards, page, pageSize, reviews.TotalRowCount);
        }

        public async Task<List<MovieCardModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.GetTop30RatedMovies();
            var movieCards = new List<MovieCardModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }

            return movieCards;
        }
    }
}

