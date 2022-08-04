using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Service;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
	public class AccountService : IAccountService
	{
        private readonly IUserRepository _userRepository;
        public AccountService(IUserRepository userRepository)
		{
            _userRepository = userRepository;
		}

        private string GetRandomSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPasswordWithSalt(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }

        public async Task<bool> CreateUser(UserRegisterModel model)
        {
            var user = await _userRepository.GetUserByEamil(model.Email);

            if (user != null)
            {
                throw new Exception("Email already exists, please try to login");
            }

            var salt = GetRandomSalt();
            var hashedPassword = GetHashedPasswordWithSalt(model.Password, salt);
            var dbUser = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };
            var savedUser = await _userRepository.AddUser(dbUser);

            if (savedUser.Id > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<UserInfoResponseModel> ValidateUser(UserLoginModel model)
        {
            var dbUser = await _userRepository.GetUserByEamil(model.Email);

            if (dbUser == null)
            {
                throw new Exception("Please register first");
            }

            var hashedPassword = GetHashedPasswordWithSalt(model.Password, dbUser.Salt);
            if (hashedPassword == dbUser.HashedPassword)
            {
                return new UserInfoResponseModel { Id = dbUser.Id, FirstName = dbUser.FirstName, LastName = dbUser.LastName, Email = dbUser.Email };
            }
            return null;
        }

        public async Task<bool> EmailExists(string email)
        {
            var dbUser = await _userRepository.GetUserByEamil(email);
            if (dbUser == null)
            {
                return false;
            }
            return true;
        }
    }
}

