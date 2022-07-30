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

        [HttpGet]
        public async Task<IActionResult> BuyMovie(int movieId, decimal price)
        {
            PurchaseRequestModel model = new PurchaseRequestModel
            {
                MovieId = movieId,
                TotalPrice = price,
                UserId = _currentUser.UserId
            };

            bool purchaseResult = await _userService.PurchaseMovie(model);
            return RedirectToAction("Details", "Movies", new { id = model.MovieId });
        }

        [HttpGet]
        public async Task<IActionResult> FavoriteMovie(int movieId)
        {

            FavoriteRequestModel model = new FavoriteRequestModel { UserId = _currentUser.UserId, MovieId = movieId };
            await _userService.AddFavorite(model);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }
        [HttpGet]
        public async Task<IActionResult> RemoveFavorite(int movieId)
        {
            FavoriteRequestModel model = new FavoriteRequestModel { UserId = _currentUser.UserId, MovieId = movieId };
            await _userService.RemoveFavorite(model);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }
    }
}

