using System.Data.Entity.Core;
using API.Users.UseCases.Get.Presenter;
using Domain.Users;
using Infrastructure;
using Infrastructure.Users;
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
    private readonly IGetUserWithOrder _getUserWithOrder; 
    private readonly UserManager<User> _userManager;

    public GetUserByIdController(
        IRepository<User> userRepository,
        UserManager<User> userManager,
        IGetUserWithOrder getUserWithOrder
    )
    {
        this._userRepository = userRepository;
        this._userManager = userManager;
        this._getUserWithOrder = getUserWithOrder;
    }

    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var user = await _getUserWithOrder.GetUserWithOrder(id);
            return Ok(await UserPresenter.PresentWithOrders(user, this._userManager));
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