using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Application.Services;
using Domain.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly KnifeService _knifeService;
    private readonly FasteningService _fasteningService;
    private readonly DeliveryTypeService _deliveryTypeService;

    public OrderController(OrderService service, KnifeService knifeService, DeliveryTypeService deliveryTypeService, FasteningService fasteningService)
    {
        _orderService = service;
        _knifeService = knifeService;
        _deliveryTypeService = deliveryTypeService;
        _fasteningService = fasteningService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        return Ok(await _orderService.GetAllOrders());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrdersById(Guid id)
    {
        try
        {
            return Ok( await _orderService.GetOrderById(id));
        }
        catch (Exception)
        {
            return BadRequest("Can't find order");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromForm] OrderDTO orderDto)
    {
        List<Guid> productsId = JsonSerializer.Deserialize<List<Guid>>(orderDto.ProductIdsJson) ?? new List<Guid>();

        List<int> productQuantities = JsonSerializer.Deserialize<List<int>>(orderDto.ProductQuantitiesJson) ?? new List<int>();

        if(productsId.Count != productQuantities.Count)
        {
            throw new Exception("Products and quantities are different");
        }

        List<Product> products = new List<Product>();

        foreach(var productId in productsId)
        {
            Product product = await _knifeService.GetKnifeById(productId) 
                              ?? (Product?)await _fasteningService.GetFasteningById(productId) 
                              ?? throw new ArgumentException("Not valid Id");
                
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

    [HttpPatch("update/status/{id:guid}")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromForm] string status)
    {
        try
        {
            return Ok(await _orderService.ChangeStatus(id, status));
        }
        catch (Exception)
        {
            return BadRequest("Can't find order");
        }
    }

    [HttpPatch("update/delivery-data/{id:guid}")]
    public async Task<IActionResult> UpdateOrderDeliveryData(Guid id, [FromForm] DeliveryDataDTO data)
    {
        try
        {
            return Ok(await _orderService.ChangeDeliveryData(id, data));
        }
        catch (Exception)
        {
            return BadRequest("Can't find order");
        }
    }

    [HttpPatch("update/delivery-type/{id:guid}")]
    public async Task<IActionResult> UpdateOrderDeliveryType(Guid id, [FromForm] DeliveryType type)
    {
        try
        {
            return Ok(await _orderService.ChangeDeliveryType(id, type));
        }
        catch (Exception)
        {
            return BadRequest("Can't find order");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await _orderService.DeleteOrder(id) });
        }
        catch (Exception)
        {
            return BadRequest("Can't find order");
        }
    }
}