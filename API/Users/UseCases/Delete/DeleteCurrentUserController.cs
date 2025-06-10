using System.Security.Claims;
using Domain.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Users.UseCases.Delete;

[ApiController]
[Route("api/users")]
public class DeleteCurrentUserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public DeleteCurrentUserController(
        UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    [HttpDelete("me")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteCurrentUser()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID claim not found in token." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User associated with this token not found." });
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(500, new { message = "Failed to delete user", errors = result.Errors });
            }

            return Ok(new { message = "User deleted successfully" });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "An unexpected error occurred while deleting the user.", error = e.Message });
        }
    }
}