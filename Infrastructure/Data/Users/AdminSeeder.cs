using Domain.Orders.Support;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Users;

public class AdminSeeder : ISeeder
{
    public int Priority => 2;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public AdminSeeder(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        var existingAdmin = await _userManager.FindByEmailAsync("admin@example.com");
        if (existingAdmin != null)
        {
            return;
        }

        Admin defaultAdmin = new Admin(
            new Guid("906a1671-2648-4fa2-85e6-2bd521928b8b"),
            "admin@example.com",
            new ClientData(
                "Default Admin",
                "+380987654321",
                "Ukraine",
                "Kyiv",
                "admin@example.com",
                "Admin Street 456",
                "01002"
            )
        );

        var result = await _userManager.CreateAsync(defaultAdmin, "Admin123!");
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
        }
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(defaultAdmin, "Admin");
        }
    }
}