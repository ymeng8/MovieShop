using System;
namespace ApplicationCore.Models
{
	public class UserDetailsModel
	{
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public ICollection<UserRoleModel>? Roles { get; set; }

        public UserDetailsModel()
        {
            Roles = new List<UserRoleModel>();
        }
    }
}

