using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly OrderStatusesService _orderStatusService;

        public OrderStatusController(OrderStatusesService service)
        {
            _orderStatusService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderStatusess()
        {
            return Ok(new { orderStatuss = await _orderStatusService.GetAllOrderStatusess() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderStatusessById(Guid id)
        {
            return Ok(new { status = await _orderStatusService.GetOrderStatusesById(id) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderStatuses([FromForm] OrderStatuses newStatus)
        {
            return Ok(new { createdStatus = await _orderStatusService.CreateOrderStatuses(newStatus) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatuses(Guid id, [FromForm] OrderStatuses updatedStatus)
        {
            return Ok(new { updatedStatus = await _orderStatusService.UpdateOrderStatuses(id, updatedStatus) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatuses(Guid id)
        {
            return Ok(new { isDeleted = await _orderStatusService.DeleteOrderStatuses(id) });
        }
    }
}
