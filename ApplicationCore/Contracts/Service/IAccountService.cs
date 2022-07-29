using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Service
{
	public interface IAccountService
	{
		Task<bool> CreateUser(UserRegisterModel model);
		Task<UserInfoResponseModel> ValidateUser(UserLoginModel model);
	}
}

