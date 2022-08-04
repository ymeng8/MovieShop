using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopAPI
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("top-grossing")]
        public async Task<IActionResult> GetTopGrossingMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();
            if (movies == null || !movies.Any())
            {
                return NotFound(new { errorMessage = "No movies found" });
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = $"No movie found for {id}" });
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllMovies(string title = "", [FromQuery] int pageSize = 30, [FromQuery] int page = 1)
        {
            var movies = await _movieService.GetMoviesByTitlePaged(title, pageSize, page);
            if (movies?.Data?.Any() == false)
            {
                return NotFound(new { errorMessage = "No movies found for search query" });
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("genre/{genreId}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId, [FromQuery] int pageSize = 30, [FromQuery] int page = 1)
        {
            var movies = await _movieService.GetMoviesByGenrePaged(genreId, pageSize, page);
            if (movies?.Data?.Any() == false)
            {
                return NotFound(new { errorMessage = "No movies found for search query" });
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("{movieId}/reviews")]
        public async Task<IActionResult> GetReviewsOfMovie(int movieId, [FromQuery] int pageSize = 30, [FromQuery] int page = 1)
        {
            var reviews = await _movieService.GetReviewsOfMoviePaged(movieId, pageSize, page);
            if (reviews?.Data?.Any() == false)
            {
                return NotFound(new { errorMessage = "No reviews found for search query" });
            }
            return Ok(reviews);
        }

        [HttpGet]
        [Route("top-rated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();
            if (movies == null || !movies.Any())
            {
                return NotFound(new { errorMessage = "No movies found" });
            }
            return Ok(movies);
        }
    }
}

