using Domain.Orders.Support;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace API.Users.UseCases.Get.Presenter;

public class UserPresenter
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public ClientData? UserData { get; set; }

    public static async Task<UserPresenter> Present(
        User user,
        UserManager<User> userManager
    )
    {
        UserPresenter userPresenter = new UserPresenter();
        userPresenter.Id = user.Id;
        userPresenter.Email = user.Email;
        userPresenter.UserData = user.UserData;
        userPresenter.Role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;
        
        return userPresenter;
    }

    public static async Task<List<UserPresenter>> PresentList(
        List<User> users,
        UserManager<User> userManager
    )
    {
        List<UserPresenter> userPresenters = new List<UserPresenter>();
        foreach (User user in users)
        {
            UserPresenter userPresenter = await Present(user, userManager);
            userPresenters.Add(userPresenter);
        }
        return userPresenters;
    }
}