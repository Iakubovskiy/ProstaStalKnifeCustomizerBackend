using Domain.Order.Support;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Users;

public class UserSeeder : ISeeder
{
    public int Priority => 1;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UserSeeder(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        var existingUser = await _userManager.FindByEmailAsync("user@example.com");
        if (existingUser != null)
        {
            return;
        }

        User defaultUser = new User(
            new Guid("5e7ca909-b9cb-492a-9bfe-cfe2f3995e15"),
            "user@example.com",
            new ClientData(
                "Default User",
                "+380123456789",
                "Ukraine",
                "Kyiv",
                "user@example.com",
                "Main Street 123",
                "01001"
            )
        );

        var result = await _userManager.CreateAsync(defaultUser, "User123!");
        
        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
        }

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(defaultUser, "User");
        }
    }
}
