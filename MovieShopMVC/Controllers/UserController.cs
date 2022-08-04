using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using MovieShopMVC.Infra;
using ApplicationCore.Contracts.Service;

namespace MovieShopMVC.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Purchases()
        {
            var movieCards = await _userService.GetAllPurchasesForUser(_currentUser.UserId);
            return View(movieCards);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var movieCards = await _userService.GetAllFavoritesForUser(_currentUser.UserId);
            return View(movieCards);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditModel model)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie(PurchaseRequestModel purchaseRequest)
        {
            await _userService.PurchaseMovie(purchaseRequest);
            return RedirectToAction("Details", "Movies", new { id = purchaseRequest.MovieId });
        }

        [HttpPost]
        public async Task<IActionResult> FavoriteMovie(FavoriteRequestModel favoriteRequest)
        {
            await _userService.AddFavorite(favoriteRequest);
            return RedirectToAction("Details", "Movies", new { id = favoriteRequest.MovieId });
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            await _userService.RemoveFavorite(favoriteRequest);
            return RedirectToAction("Details", "Movies", new { id = favoriteRequest.MovieId });
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewRequestModel reviewRequest)
        {
            await _userService.AddMovieReview(reviewRequest);
            return RedirectToAction("Details", "Movies", new { id = reviewRequest.MovieId });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteReview(int movieId)
        {
            await _userService.DeleteMovieReview(_currentUser.UserId, movieId);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        [HttpGet]
        public async Task<IActionResult> EditReview(int id)
        {
            var reviewDetails = await _userService.GetReviewDetails(_currentUser.UserId, id);
            ReviewRequestModel editRequest = new ReviewRequestModel
            {
                MovieId = id,
                UserId = reviewDetails.UserId,
                Rating = reviewDetails.Rating,
                ReviewText = reviewDetails.ReviewText
            };
            return View(editRequest);
        }
        [HttpPost]
        public async Task<IActionResult> EditReview(ReviewRequestModel editRequest)
        {
            await _userService.UpdateMovieReview(editRequest);
            return RedirectToAction("Details", "Movies", new { id = editRequest.MovieId });
        }
    }
}

