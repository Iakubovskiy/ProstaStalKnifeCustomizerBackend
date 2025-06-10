using Domain.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Users.UseCases.Delete;

[ApiController]
[Route("api/users")]
public class DeleteUserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public DeleteUserController(
        UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound(new { message = "User not found" });

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return StatusCode(500, new { message = "Failed to delete user", errors = result.Errors });

            return Ok(new { message = "User deleted successfully" });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Failed to delete user", error = e.Message });
        }
    }
}