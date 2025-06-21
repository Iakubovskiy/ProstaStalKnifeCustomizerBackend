using System.Data.Entity.Core;
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
public class GetUserByIdController : ControllerBase
{
    private readonly IRepository<User> _userRepository;
    private readonly UserManager<User> _userManager;

    public GetUserByIdController(
        IRepository<User> userRepository,
        UserManager<User> userManager
    )
    {
        this._userRepository = userRepository;
        this._userManager = userManager;
    }

    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            return Ok(await UserPresenter.Present(user, this._userManager));
        }
        catch (ObjectNotFoundException)
        {
            return NotFound(new { message = "User not found" });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Failed to retrieve user", error = e.Message });
        }
    }

}