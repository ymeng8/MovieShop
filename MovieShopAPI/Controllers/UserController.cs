using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Service;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using MovieShopAPI.Infra;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;
        public UserController(ICurrentUser currentUser, IUserService userService)
        {
            _currentUser = currentUser;
            _userService = userService;
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            var userDetails = await _userService.GetUserDetails(id);
            if (userDetails != null)
            {
                return Ok(userDetails);
            }
            return NotFound(new { errorMessage = $"No user found for {id}" });
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> GetAllFavorites()
        {
            var userId = _currentUser.UserId;
            var favorites = await _userService.GetAllFavoritesForUser(userId);
            if (favorites == null || !favorites.Any())
            {
                return NotFound(new { errorMessage = $"No favorites found for user {userId}" });
            }
            return Ok(favorites);
        }

        [HttpGet]
        [Route("check-movie-favorite/{movieId}")]
        public async Task<IActionResult> FavoriteExists(int movieId)
        {
            var favoriteExists = await _userService.FavoriteExists(_currentUser.UserId, movieId);
            return Ok(new { favoriteExists = favoriteExists });
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.AddFavorite(favoriteRequest);
            return Ok(favoriteRequest);
        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> RemoveFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.RemoveFavorite(favoriteRequest);
            return Ok(favoriteRequest);
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetMoviesPurchasedByUser()
        {
            var userId = _currentUser.UserId;
            var purchases = await _userService.GetAllPurchasesForUser(userId);
            if (purchases == null || !purchases.Any())
            {
                return NotFound(new { errorMessage = $"No purchase found for user {userId}" });
            }
            return Ok(purchases);
        }

        [HttpGet]
        [Route("purchase-details/{movieId}")]
        public async Task<IActionResult> GetPurchaseDetails(int movieId)
        {
            var purchaseDetails = await _userService.GetPurchasesDetails(_currentUser.UserId, movieId);
            if (purchaseDetails != null)
            {
                return Ok(purchaseDetails);
            }
            return NotFound(new { errorMessage = $"No purchase found for {movieId}" });
        }

        [HttpGet]
        [Route("check-movie-purchased/{movieId}")]
        public async Task<IActionResult> PurchaseExists(int movieId)
        {
            var purchaseExists = await _userService.IsMoviePurchased(movieId, _currentUser.UserId);
            return Ok(new { purchaseExists = purchaseExists });
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> BuyMovie([FromBody] PurchaseRequestModel purchaseRequest)
        {
            await _userService.PurchaseMovie(purchaseRequest);
            return Ok(purchaseRequest);
        }

        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var userId = _currentUser.UserId;
            var reviews = await _userService.GetAllReviewsByUser(userId);
            if (reviews == null || !reviews.Any())
            {
                return NotFound(new { errorMessage = $"No review found for user {userId}" });
            }
            return Ok(reviews);

        }

        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequestModel reviewRequest)
        {
            await _userService.AddMovieReview(reviewRequest);
            return Ok(reviewRequest);
        }

        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> EditReview([FromBody] ReviewRequestModel reviewRequest)
        {
            await _userService.UpdateMovieReview(reviewRequest);
            return Ok(reviewRequest);
        }

        [HttpDelete]
        [Route("delete-review/{movieId}")]
        public async Task<IActionResult> DeleteReview(int movieId)
        {
            await _userService.DeleteMovieReview(_currentUser.UserId, movieId);
            return Ok();
        }
    }
}

