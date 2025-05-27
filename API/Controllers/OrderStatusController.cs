using Domain.Order.Support;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderStatusController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllOrderStatuses()
    {
        var statuses = Enum.GetNames(typeof(OrderStatuses));
        return Ok(statuses);
    }
}