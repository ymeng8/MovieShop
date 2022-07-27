﻿using System;
namespace ApplicationCore.Models
{
	public class UserRegisterModel
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

