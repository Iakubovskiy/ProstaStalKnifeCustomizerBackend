using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Users.UseCases.Get;

[ApiController]
[Route("api/users")]
public class GetAllUsersController : ControllerBase
{
    private readonly IRepository<User> _userRepository;

    public GetAllUsersController(IRepository<User> userRepository)
    {
        this._userRepository = userRepository;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userRepository.GetAll());
    }
}