using API.Users.UseCases.Get.Presenter;
using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Users.UseCases.Get;

[ApiController]
[Route("api/users")]
public class GetAllUsersController : ControllerBase
{
    private readonly IRepository<User> _userRepository;
    private readonly UserManager<User> _userManager;

    public GetAllUsersController(
        IRepository<User> userRepository,
        UserManager<User> userManager
    )
    {
        this._userRepository = userRepository;
        this._userManager = userManager;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await UserPresenter.PresentList(await _userRepository.GetAll(), this._userManager));
    }
}