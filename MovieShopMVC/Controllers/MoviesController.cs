﻿using System;
using ApplicationCore.Contracts.Service;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
	public class MoviesController : Controller
	{
		private readonly IMovieService _movieService;
		public MoviesController(IMovieService movieService)
		{
			_movieService = movieService;
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var movieDetails = await _movieService.GetMovieDetails(id);
			return View(movieDetails);
		}
	}
}

