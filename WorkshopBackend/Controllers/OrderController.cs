using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
        private readonly KnifeService _knifeService;
        private readonly OrderStatusesService _statusService;
        private readonly DeliveryTypeService _deliveryTypeService;

        public OrderController(OrderService service, KnifeService knifeService, OrderStatusesService statusService, DeliveryTypeService deliveryTypeService)
        {
            _orderService = service;
            _knifeService = knifeService;
            _statusService = statusService;
            _deliveryTypeService = deliveryTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersById(int id)
        {
            return Ok( await _orderService.GetOrderById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm] OrderDTO orderDto)
        {
            var knives = !string.IsNullOrEmpty(orderDto.KnivesIdsJson)
            ? JsonSerializer.Deserialize<List<int>>(orderDto.KnivesIdsJson)
                .Select(id => _knifeService.GetKnifeById(id).Result)
                .ToList()
            : new List<Knife>();
            var deliveryType = await _deliveryTypeService.GetDeliveryTypeById(orderDto.DeliveryTypeId);
            var status = await _statusService.GetOrderStatusesById(orderDto.StatusId);
            Order order = new Order
            {
                Id = orderDto.Id,
                City = orderDto.City,
                ClientFullName = orderDto.ClientFullName,
                ClientPhoneNumber = orderDto.ClientPhoneNumber,
                Comment = orderDto.Comment,
                CountryForDelivery = orderDto.CountryForDelivery,
                DeliveryType = deliveryType,
                Email = orderDto.Email,
                Knives = knives,
                Number = orderDto.Number,
                Status = status,
                Total = orderDto.Total,
            };
            return Ok(new { createdColor = await _orderService.CreateOrder(order) });
        }

        [HttpPatch("update/status/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromForm] string status)
        {
            return Ok(await _orderService.ChangeStatus(id, status));
        }

        [HttpPatch("update/delivery-data/{id}")]
        public async Task<IActionResult> UpdateOrderDeliveryData(int id, [FromForm] DeliveryDataDTO data)
        {
            return Ok(await _orderService.ChangeDeliveryData(id, data));
        }

        [HttpPatch("update/delivery-type/{id}")]
        public async Task<IActionResult> UpdateOrderDeliveryType(int id, [FromForm] DeliveryType type)
        {
            return Ok(await _orderService.ChangeDeliveryType(id, type));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(new { isDeleted = await _orderService.DeleteOrder(id) });
        }
    }
}
