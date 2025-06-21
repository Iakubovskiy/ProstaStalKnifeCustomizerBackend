using Microsoft.AspNetCore.Mvc;
using Application.Users.UseCases.Registration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Users.UseCases.Register;

[Route("api/")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationController(IRegistrationService registrationService)
    {
        this._registrationService = registrationService;
    }
    
    [HttpPost("register")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> Register([FromBody] RegisterDto registrationDto)
    {
        if (string.IsNullOrEmpty(registrationDto.Role) || registrationDto.Role.ToLower() != "user")
            return StatusCode(StatusCodes.Status403Forbidden, new { message = "Guests can only register as User" });

        try
        {
            var user = await _registrationService.Register(registrationDto);
            return Ok(new { message = "User registered successfully", userId = user.Id });
        }
        catch (ArgumentException e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new { message = e.Message });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost("admin/register")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> RegisterByAdmin([FromBody] RegisterDto registrationDto)
    {
        try
        {
            var user = await _registrationService.Register(registrationDto);
            return Ok(new { message = "User registered successfully by admin", userId = user.Id });
        }
        catch (ArgumentException e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new { message = e.Message });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}