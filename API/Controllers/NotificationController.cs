using Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly ICustomEmailSender _customEmailSender;

    public NotificationController(ICustomEmailSender customEmailSender)
    {
        _customEmailSender = customEmailSender;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] EmailDTO emailData)
    {
        if (string.IsNullOrWhiteSpace(emailData.EmailTo) || string.IsNullOrWhiteSpace(emailData.EmailSubject))
        {
            return BadRequest(new { message = "Некоректні вхідні дані" });
        }
        try
        {
            await _customEmailSender.SendEmailAsync(emailData);
            return Ok(new { message = "Email успішно відправлено" });
        }
        catch (Exception ex)
        {
            return StatusCode(400, new { message = "Помилка при відправленні email", error = ex.Message });
        }
    }
}