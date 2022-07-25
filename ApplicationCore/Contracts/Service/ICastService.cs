using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Service
{
	public interface ICastService
	{
		Task<CastDetailsModel> GetCastDetails(int castId);
	}
}

