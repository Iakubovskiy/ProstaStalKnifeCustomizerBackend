using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.UseCases.Registration;

public class RegistrationService : IRegistrationService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RegistrationService(
        UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager
    )
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
    }

    public async Task<User> Register(RegisterDto registerDto)
    {
        if (string.IsNullOrEmpty(registerDto.Password) || registerDto.Password != registerDto.PasswordConfirmation)
        {
            throw new ArgumentException("Password is empty or it doesn't match confirmation password");
        }

        Guid userId = registerDto.Id ?? Guid.NewGuid();
        User newUser;

        if (registerDto.Role.ToLower() == "user")
        {
            newUser = new User(
                userId,
                registerDto.Email
            );
        }
        else if (registerDto.Role.ToLower() == "admin")
        {
            newUser = new Admin(
                userId,
                registerDto.Email
            );
        }
        else
        {
            throw new ArgumentException("Invalid role");
        }

        try
        {
            await this._userManager.CreateAsync(newUser, registerDto.Password);
            if (registerDto.Role.ToLower() == "user")
                await this._userManager.AddToRoleAsync(newUser, "User");
            else if (registerDto.Role.ToLower() == "admin")
                await this._userManager.AddToRoleAsync(newUser, "Admin");
        }
        catch (Exception e)
        {
            throw new Exception("Registration failed", e);
        }
        return newUser;
    }
}