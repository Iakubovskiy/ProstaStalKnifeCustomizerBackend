using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.DTO;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController([FromForm] OrderService service)
        {
            _orderService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(new { orders = await _orderService.GetAllOrders() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersById(int id)
        {
            return Ok(new { color = await _orderService.GetOrderById(id) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm] Order color)
        {
            return Ok(new { createdColor = await _orderService.CreateOrder(color) });
        }

        [HttpPatch("update/status/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromForm] string status)
        {
            return Ok(new { updatedColor = await _orderService.ChangeStatus(id, status) });
        }

        [HttpPatch("update/delivery-data/{id}")]
        public async Task<IActionResult> UpdateOrderDeliveryData(int id, [FromForm] DeliveryDataDTO data)
        {
            return Ok(new { updatedColor = await _orderService.ChangeDeliveryData(id, data) });
        }

        [HttpPatch("update/delivery-type/{id}")]
        public async Task<IActionResult> UpdateOrderDeliveryType(int id, [FromForm] DeliveryType type)
        {
            return Ok(new { updatedColor = await _orderService.ChangeDeliveryType(id, type) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(new { isDeleted = await _orderService.DeleteOrder(id) });
        }
    }
}
