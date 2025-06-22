using System.Data.Entity.Core;
using System.Net;
using System.Security.Claims;
using API.Orders.Presenters;
using API.Orders.Support;
using Application.Components.Prices;
using Application.Currencies;
using Microsoft.AspNetCore.Mvc;
using Application.Orders.Dto;
using Application.Orders.UseCases.ChangeClientData;
using Application.Orders.UseCases.Create;
using Application.Orders.UseCases.RemoveOrderItem;
using Application.Orders.UseCases.UpdateOrderItemQuantity;
using Application.Orders.UseCases.UpdateStatus;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Domain.Orders.Support;
using Domain.Orders.Support.Validators;
using Infrastructure.Components;
using Infrastructure.Orders;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Orders;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICreateOrderService _createOrderService;
    private readonly IUpdateOrderStatusService _updateOrderStatusService;
    private readonly IChangeClientDataService _changeClientDataService;
    private readonly IRemoveOrderItem _removeOrderItem;
    private readonly IUpdateOrderItemQuantityService _updateOrderItemQuantityService;
    private readonly IPriceService _priceService;
    private readonly IGetComponentPrice _getComponentPrice;
    private readonly IComponentRepository<Attachment> _attachmentRepository;
        private readonly IComponentRepository<Knife> _knifeRepository;
    private readonly IComponentRepository<CompletedSheath> _completedSheathRepository;

    public OrderController(
        IOrderRepository orderRepository, 
        ICreateOrderService createOrderService, 
        IUpdateOrderStatusService updateOrderStatusService, 
        IChangeClientDataService changeClientDataService,
        IRemoveOrderItem removeOrderItem,
        IUpdateOrderItemQuantityService updateOrderItemQuantityService,
        IPriceService priceService,
        IGetComponentPrice getComponentPrice,
        IComponentRepository<Attachment> attachmentRepository,
        IComponentRepository<Knife> knifeRepository,
        IComponentRepository<CompletedSheath> completedSheathRepository
    )
    {
        this._orderRepository = orderRepository;
        this._createOrderService = createOrderService;
        this._updateOrderStatusService = updateOrderStatusService;
        this._changeClientDataService = changeClientDataService;
        this._removeOrderItem = removeOrderItem;
        this._updateOrderItemQuantityService = updateOrderItemQuantityService;
        this._priceService = priceService;
        this._getComponentPrice = getComponentPrice;
        this._attachmentRepository = attachmentRepository;
        this._knifeRepository = knifeRepository;
        this._completedSheathRepository = completedSheathRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await OrderPresenter
            .PresentList(
                await this._orderRepository.GetAll(), 
                locale, 
                currency, 
                this._priceService, 
                this._getComponentPrice,
                this._attachmentRepository,
                this._knifeRepository,
                this._completedSheathRepository
            )
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(
        Guid id,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        try
        {
            return Ok(await OrderPresenter
                .Present(
                    await this._orderRepository.GetById(id), 
                    locale, 
                    currency, 
                    this._priceService, 
                    this._getComponentPrice,
                    this._attachmentRepository,
                    this._knifeRepository,
                    this._completedSheathRepository
                ));
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
            var authResult = await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            if (authResult.Succeeded)
            {
                HttpContext.User = authResult.Principal;
            }
            string? userId = null;
            if (User.Identity?.IsAuthenticated == true)
            {
                userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            Guid? userIdGuid = null;
            if (!string.IsNullOrEmpty(userId))
            {
                userIdGuid = new Guid(userId);
            }
            return Created(nameof(this.GetOrderById),
                new { newOrder = await this._createOrderService.Create(orderDto, locale, userIdGuid) });
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
        [FromBody] ClientDataDto dto
    )
    {
        try
        {
            ClientData clientData = new ClientData(
                dto.ClientFullName,
                dto.ClientPhoneNumber,
                dto.CountryForDelivery,
                dto.City,
                dto.Email,
                dto.Address,
                dto.ZipCode
            );
            return Ok(await this._changeClientDataService.ChangeClientData(id, clientData));
        }
        catch (ObjectNotFoundException)
        {
            return NotFound("Can't find order");
        }
        catch (ArgumentException e)
        {
            return StatusCode(400, e.Message);
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
    
    [HttpPatch("{orderId}/items/{productId}/quantity")]
    public async Task<IActionResult> UpdateQuantity(Guid orderId, Guid productId, [FromBody] int quantity)
    {
        await _updateOrderItemQuantityService.UpdateQuantity(orderId, productId, quantity);
        return NoContent();
    }

    [HttpDelete("{orderId}/items/{productId}")]
    public async Task<IActionResult> RemoveItem(Guid orderId, Guid productId)
    {
        await _removeOrderItem.RemoveOrderItem(orderId, productId);
        return NoContent();
    }
}