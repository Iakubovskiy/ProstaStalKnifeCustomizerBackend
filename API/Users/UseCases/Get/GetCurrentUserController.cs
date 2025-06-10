using System.Security.Claims;
using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Users.UseCases.Get;

[ApiController]
[Route("api/users")]
public class GetCurrentUserController : ControllerBase
{
    private readonly IRepository<User> _userRepository;

    public GetCurrentUserController(IRepository<User> userRepository)
    {
        this._userRepository = userRepository;
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

            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(400, new { message = "Failed to retrieve current user", error = e.Message });
        }
    }
}