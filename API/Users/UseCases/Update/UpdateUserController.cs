using Application.Users.UseCases.Update;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Users.UseCases.Update;

[ApiController]
[Route("api/users/")]
public class UpdateUserController : ControllerBase
{
    private readonly IUpdateUserService _updateUserService;

    public UpdateUserController(IUpdateUserService updateUserService)
    {
        this._updateUserService = updateUserService;
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [SwaggerOperation(Tags = new[] { "Users" })]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto updateDto)
    {
        if (updateDto == null)
            return BadRequest("Update data is required");

        try
        {
            var updatedUser = await _updateUserService.UpdateAsync(id, updateDto);
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
            return StatusCode(500, new { message = "Failed to update user", error = e.Message });
        }
    }
}