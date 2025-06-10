using System.Security.Claims;
using Application.Users.UseCases.Update;
using Domain.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Users.UseCases.Update;

[ApiController]
[Route("api/users/")]
public class UpdateCurrentUserController : ControllerBase
{
    private readonly IUpdateUserService _updateUserService;

    public UpdateCurrentUserController(IUpdateUserService updateUserService)
    {
        this._updateUserService = updateUserService;
    }
    [HttpPut("me")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserDto updateDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User not authenticated" });

            var updatedUser = await _updateUserService.UpdateAsync(Guid.Parse(userId), updateDto);
            return Ok(new { message = "User updated successfully", user = updatedUser });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "User not found" });
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(400, new { message = "Failed to update user", error = e.Message });
        }
    }
}