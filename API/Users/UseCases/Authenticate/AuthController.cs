using System.Data.Entity.Core;
using Application.Users.UseCases.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Users.UseCases.Authenticate;

[Route("api")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        this._authService = authService;
    }

    [HttpPost("login")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            string token = await _authService.Login(model);
            return Ok(new { Token = token });
        }
        catch (ObjectNotFoundException)
        {
            return Unauthorized();
        }
    }

    [HttpPost ("logout")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        await HttpContext.SignOutAsync();
        return Ok(new { status = 200 });
    }
}