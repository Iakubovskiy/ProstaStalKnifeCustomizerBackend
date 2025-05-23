using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Users.Authentication.Models;
using Microsoft.Extensions.Configuration;
using Domain.Users;

namespace Application.Users.Authentication
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<(bool, string? role)> ValidateUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username) ?? await _userManager.FindByEmailAsync(username);
            if (user == null)
                return (false, "");

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                return (true, role);
            }

            return (false, "");
        }

        public string GenerateJwtTokenAsync(User user, string role, IEnumerable<Claim> claims)
        {
            var enumerable = claims.ToList();
            var claimsList = enumerable.ToList();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? _configuration.GetValue<string>("Jwt_Key") ?? throw new NullReferenceException()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? _configuration.GetValue<string>("Jwt_Issuer"),
                audience: _configuration["Jwt:Issuer"] ?? _configuration.GetValue<string>("Jwt_Issuer"),
                claims: claimsList,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine($"Generated token: {tokenString}");

            return tokenString;
        }

        public async Task<string> Login(LoginDTO model)
        {
            var user = null as User;
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                   throw new Exception("User not found");
                }
            }
            else
            {
                var emailUser = await _userManager.FindByEmailAsync(model.Username);
                if(emailUser != null)
                {
                    var emailResult = await _signInManager.PasswordSignInAsync(emailUser.UserName, model.Password, false, lockoutOnFailure: false);
                    if (emailResult.Succeeded)
                    {
                        user = await _userManager.FindByNameAsync(emailUser.UserName);
                        if (user == null)
                        {
                            throw new Exception("User not found");
                        }
                    }
                }
            }
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId", user.Id),
            };
            var token = this.GenerateJwtTokenAsync(user, role, claims);
            
            return token;
        }
    }
}
