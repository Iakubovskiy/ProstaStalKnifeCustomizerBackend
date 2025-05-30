using Microsoft.AspNetCore.Mvc;
using Application.Users.UseCases.Registration;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationController(IRegistrationService registrationService)
    {
        this._registrationService = registrationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterDto registrationDto)
    {
        try
        {
            await this._registrationService.Register(registrationDto);

            return Ok(new { status = 200, message = "User registered successfully" });
        }
        catch (Exception e)
        {
            return BadRequest($"User already exists. | { e.Message }");
        }
    }
}