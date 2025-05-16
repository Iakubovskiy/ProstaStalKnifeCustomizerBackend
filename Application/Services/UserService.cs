using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(IdentityResult Result, string UserId)> RegisterUser(string username, string password, 
            string role, string email, string phoneNumber)
        {
            User user;
            if (role == "User")
            {
                user = new User
                {
                    UserName = username,
                    Email = email,
                    PhoneNumber = phoneNumber,
                };        
            }
            else if (role == "Admin")
            {
                user = new Admin
                {
                    UserName = username,
                    Email = email,
                    PhoneNumber = phoneNumber,
                };
            }
            else
            {
                throw new ArgumentException("Invalid role specified");
            }

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return (result, user.Id);
        }
    }

}
