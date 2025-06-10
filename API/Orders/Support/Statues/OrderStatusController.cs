using Domain.Order.Support;
using Microsoft.AspNetCore.Mvc;

namespace API.Orders.Support.Statues;

[Route("api/order-statuses")]
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