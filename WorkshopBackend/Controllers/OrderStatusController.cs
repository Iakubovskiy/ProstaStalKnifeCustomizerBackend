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
        public async Task<IActionResult> GetOrderStatusessById(int id)
        {
            return Ok(new { status = await _orderStatusService.GetOrderStatusesById(id) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderStatuses([FromForm] OrderStatuses status)
        {
            return Ok(new { createdStatus = await _orderStatusService.CreateOrderStatuses(status) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatuses(int id, [FromForm] OrderStatuses status)
        {
            return Ok(new { updatedStatus = await _orderStatusService.UpdateOrderStatuses(id, status) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatuses(int id)
        {
            return Ok(new { isDeleted = await _orderStatusService.DeleteOrderStatuses(id) });
        }
    }
}
