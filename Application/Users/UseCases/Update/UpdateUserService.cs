using Application.Users.UseCases.Registration;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.UseCases.Update;

public class UpdateUserService : IUpdateUserService
{
    private readonly UserManager<User> _userManager;

    public UpdateUserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> UpdateAsync(Guid id, UpdateUserDto dto)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new KeyNotFoundException("User not found");

        if (!string.IsNullOrEmpty(dto.Password))
        {
            if (dto.Password != dto.PasswordConfirmation)
                throw new ArgumentException("Passwords don't match");
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResult = await _userManager.ResetPasswordAsync(user, token, dto.Password);
            if (!passwordResult.Succeeded)
                throw new InvalidOperationException("Failed to update password");
        }

        User updateData = user is Admin 
            ? new Admin(user.Id, dto.Email, dto.ClientData)
            : new User(user.Id, dto.Email, dto.ClientData);

        user.Update(updateData);

        var result = await _userManager.UpdateAsync(user);
        
        if (!result.Succeeded)
            throw new InvalidOperationException("Failed to update user");

        return user;
    }
}
