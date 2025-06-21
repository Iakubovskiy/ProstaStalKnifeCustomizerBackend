using System.Security.Claims;
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
public class GetCurrentUserController : ControllerBase
{
    private readonly IRepository<User> _userRepository;
    private readonly UserManager<User> _userManager;

    public GetCurrentUserController(
        IRepository<User> userRepository,
        UserManager<User> userManager
    )
    {
        this._userRepository = userRepository;
        this._userManager = userManager;
    }
    
    [HttpGet("me")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User not authenticated" });

            var user = await _userRepository.GetById(Guid.Parse(userId));
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(await UserPresenter.Present(user, this._userManager));
        }
        catch (Exception e)
        {
            return StatusCode(400, new { message = "Failed to retrieve current user", error = e.Message });
        }
    }
}