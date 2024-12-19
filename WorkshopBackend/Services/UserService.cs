using WorkshopBackend.Data;
using WorkshopBackend.Models;
using Microsoft.AspNetCore.Identity;

namespace WorkshopBackend.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly HttpClient _httpClient;
        private readonly DBContext _context;

        public UserService(UserManager<User> userManager, 
            IHttpClientFactory httpClient, DBContext dbContext)
        {
            _userManager = userManager;
            _passwordHasher = new PasswordHasher<User>();
            _httpClient = httpClient.CreateClient("Api");
            _context = dbContext;
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
        /*public async Task<IdentityResult> UpdateUser(string? userId, string? username, 
            string? password, string? role, string? name, string? surname, DateTime? dateOfBirth, 
            string? phoneNumber, decimal? pricePerHour, IFormFile? Photo)
        {

            User existingUser = await _userManager.FindByIdAsync(userId);
            bool isInRole = await _userManager.IsInRoleAsync(existingUser, "PSYCHOLOGIST");
            if (username != null)
            {
                existingUser.UserName = username;
                existingUser.NormalizedUserName = username.ToUpper();
            }
            if (password != null)
            {
                password = _passwordHasher.HashPassword(existingUser, password);
                existingUser.PasswordHash = password;
            }
            existingUser.Name = name!=null? name:existingUser.Name;
            existingUser.Surname = surname!=null? surname:existingUser.Surname;
            if (dateOfBirth != null)
            {
                existingUser.DateOfBirth = dateOfBirth.Value;
            }
            existingUser.PhoneNumber = phoneNumber!=null? phoneNumber:existingUser.PhoneNumber;
            decimal pricePerHourValid;
            if (pricePerHour != null && decimal.TryParse(pricePerHour.Value.ToString(), out pricePerHourValid) && isInRole) 
            {
                Psychologist psychologist = await _context.Psychologists.FirstOrDefaultAsync(p => p.Id==userId);
                psychologist.PricePerHour = pricePerHourValid;
                await _userManager.UpdateAsync(psychologist);
            }
           
            IFormFile? photo = Photo;
            if (photo != null)
            {
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(photo.OpenReadStream()), "photo", photo.FileName);
                var photoResponse = await _httpClient.PostAsync($"PhotoManagement/Photo/Upload?userId={userId}", content);
            }
            var result = await _userManager.UpdateAsync(existingUser);

            return result;
        }*/
    }

}
