using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.DTO;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly UserService _userService;

        public RegistrationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO model)
        {
            var (result, userId) = await _userService.RegisterUser(model.Username, model.Password, model.Role, 
                model.Email, model.PhoneNumber);

            if (result.Succeeded)
            {
                return Ok(new { status = 200, message = "User registered successfully" });
            }
            return BadRequest("User already exists");
        }
    }
}
