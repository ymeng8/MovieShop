using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Service;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
	public class CastService : ICastService
	{
        private readonly ICastRepository _castRepository;
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsModel> GetCastDetails(int castId)
        {
            var castDetails = await _castRepository.GetById(castId);
            string gender;
            if (castDetails.Gender == "1")
            {
                gender = "Female";
            }
            else if (castDetails.Gender == "2")
            {
                gender = "Male";
            }
            else if (castDetails.Gender == "3")
            {
                gender = "Non-binary";
            }
            else
            {
                gender = "Unknown";
            }
            var castDetailsModel = new CastDetailsModel
            {
                Id = castDetails.Id,
                Name = castDetails.Name,
                Gender = gender,
                ProfilePath = castDetails.ProfilePath,
                TmdbUrl = castDetails.TmdbUrl
            };
            foreach (var movie in castDetails.MoviesOfCast)
            {
                castDetailsModel.Movies.Add(new MovieCardModel { Id = movie.Movie.Id, Title = movie.Movie.Title, PosterUrl = movie.Movie.PosterUrl });
            }
            return castDetailsModel;
        }
    }
}

