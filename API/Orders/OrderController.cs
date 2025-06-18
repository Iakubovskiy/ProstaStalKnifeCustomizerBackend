using System.Data.Entity.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Application.Orders.Dto;
using Application.Orders.UseCases.ChangeClientData;
using Application.Orders.UseCases.Create;
using Application.Orders.UseCases.UpdateStatus;
using Domain.Component.Product;
using Domain.Order;
using Domain.Order.Support;
using Infrastructure.Orders;

namespace API.Orders;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICreateOrderService _createOrderService;
    private readonly IUpdateOrderStatusService _updateOrderStatusService;
    private readonly IChangeClientDataService _changeClientDataService;

    public OrderController(
        IOrderRepository orderRepository, 
        ICreateOrderService createOrderService, 
        IUpdateOrderStatusService updateOrderStatusService, 
        IChangeClientDataService changeClientDataService
    )
    {
        this._orderRepository = orderRepository;
        this._createOrderService = createOrderService;
        this._updateOrderStatusService = updateOrderStatusService;
        this._changeClientDataService = changeClientDataService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        return Ok(await this._orderRepository.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrdersById(Guid id)
    {
        try
        {
            return Ok( await this._orderRepository.GetById(id));
        }
        catch (ObjectNotFoundException)
        {
            return NotFound("Can't find order");
        }
        catch (Exception e)
        {
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromHeader] string locale,[FromBody] OrderDto orderDto)
    {
        try
        {
            return Ok(new { newOrder = await this._createOrderService.Create(orderDto, locale) });
        }
        catch (Exception e)
        {
            return StatusCode(400, e.Message);
        }
    }

    [HttpPatch("update/status/{id:guid}")]
    public async Task<IActionResult> UpdateOrderStatus(
        Guid id, 
        [FromBody] string status
    )
    {
        try
        {
            return Ok(await this._updateOrderStatusService.UpdateStatus(id, status));
        }
        catch (ObjectNotFoundException)
        {
            return NotFound("Can't find order");
        }
        catch (Exception e)
        {
            return StatusCode(400, e.Message);
        }
    }

    [HttpPatch("update/delivery-data/{id:guid}")]
    public async Task<IActionResult> UpdateOrderDeliveryData(
        Guid id, 
        [FromBody] ClientData clientData
    )
    {
        try
        {
            return Ok(await this._changeClientDataService.ChangeClientData(id, clientData));
        }
        catch (Exception)
        {
            return NotFound("Can't find order");
        }
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._orderRepository.Delete(id) });
        }
        catch (ObjectNotFoundException)
        {
            return NotFound("Can't find order");
        }
        catch (Exception e)
        {
            return StatusCode(400, e.Message);
        }
    }
}