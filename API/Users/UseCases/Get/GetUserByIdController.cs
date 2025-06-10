using System.Data.Entity.Core;
using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Users.UseCases.Get;

[ApiController]
[Route("api/users")]
public class GetUserByIdController : ControllerBase
{
    private readonly IRepository<User> _userRepository;

    public GetUserByIdController(IRepository<User> userRepository)
    {
        this._userRepository = userRepository;
    }

    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            return Ok(user);
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