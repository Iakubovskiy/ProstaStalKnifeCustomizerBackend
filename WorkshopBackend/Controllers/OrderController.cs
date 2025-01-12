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
        private readonly FasteningService _fasteningService;
        private readonly OrderStatusesService _statusService;
        private readonly DeliveryTypeService _deliveryTypeService;

        public OrderController(OrderService service, KnifeService knifeService, OrderStatusesService statusService, DeliveryTypeService deliveryTypeService, FasteningService fasteningService)
        {
            _orderService = service;
            _knifeService = knifeService;
            _statusService = statusService;
            _deliveryTypeService = deliveryTypeService;
            _fasteningService = fasteningService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersById(Guid id)
        {
            return Ok( await _orderService.GetOrderById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm] OrderDTO orderDto)
        {
            var productsId = !string.IsNullOrEmpty(orderDto.ProductIdsJson)
            ? JsonSerializer.Deserialize<List<Guid>>(orderDto.ProductIdsJson):new List<Guid>();

            var productQuantities = !string.IsNullOrEmpty(orderDto.ProductIdsJson)
            ? JsonSerializer.Deserialize<List<int>>(orderDto.ProductQuantitiesJson) : new List<int>();

            if(productsId.Count != productQuantities.Count)
            {
                throw new Exception("Products and quantities are different");
            }

            List<Product> products = new List<Product>();

            foreach(var productId in productsId)
            {
                Product product = await _knifeService.GetKnifeById(productId);
                if(product == null)
                {
                    product = await _fasteningService.GetFasteningById(productId);
                }
                products.Add(product);
            }
            List<(Product, int)> items = new List<(Product, int)> ();
            for(int i = 0; i<products.Count; i++)
            {
                (Product, int) item = (products[i], productQuantities[i]);
                items.Add(item);
            }

            var deliveryType = await _deliveryTypeService.GetDeliveryTypeById(orderDto.DeliveryTypeId);
            Order order = new Order
            {
                City = orderDto.City,
                ClientFullName = orderDto.ClientFullName,
                ClientPhoneNumber = orderDto.ClientPhoneNumber,
                Comment = orderDto.Comment,
                CountryForDelivery = orderDto.CountryForDelivery,
                DeliveryType = deliveryType,
                Email = orderDto.Email,
                Products = products,
                Number = orderDto.Number,
                Status = orderDto.Status,
                Total = orderDto.Total,
            };
            return Ok(new { createdColor = await _orderService.CreateOrder(order,items) });
        }

        [HttpPatch("update/status/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromForm] string status)
        {
            return Ok(await _orderService.ChangeStatus(id, status));
        }

        [HttpPatch("update/delivery-data/{id}")]
        public async Task<IActionResult> UpdateOrderDeliveryData(Guid id, [FromForm] DeliveryDataDTO data)
        {
            return Ok(await _orderService.ChangeDeliveryData(id, data));
        }

        [HttpPatch("update/delivery-type/{id}")]
        public async Task<IActionResult> UpdateOrderDeliveryType(Guid id, [FromForm] DeliveryType type)
        {
            return Ok(await _orderService.ChangeDeliveryType(id, type));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            return Ok(new { isDeleted = await _orderService.DeleteOrder(id) });
        }
    }
}
