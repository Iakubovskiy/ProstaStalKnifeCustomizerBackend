using Application.Orders.Support.DeliveryTypes;
using Application.Orders.Support.DeliveryTypes.Data;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Orders.Support.DeliveryTypes;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeliveryTypeController : ControllerBase
{
    private readonly IDeliveryTypeRepository _deliveryTypeRepository;
    private readonly IDeliveryTypeService _deliveryTypeService;

    public DeliveryTypeController(
        IDeliveryTypeRepository deliveryTypeRepository,
        IDeliveryTypeService deliveryTypeService
    )
    {
        this._deliveryTypeRepository = deliveryTypeRepository;
        this._deliveryTypeService = deliveryTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDeliveryTypes()
    {
        return Ok(await this._deliveryTypeRepository.GetAll());
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveDeliveryTypes()
    {
        return Ok(await this._deliveryTypeRepository.GetAllActive());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDeliveryTypesById(Guid id)
    {
        try
        {
            return Ok(await this._deliveryTypeRepository.GetById(id));
        }
        catch (Exception)
        {
            return BadRequest("Can't find DeliveryType");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateDeliveryType([FromBody] DeliveryTypeDto type)
    {
        return Ok(await this._deliveryTypeService.Create(type));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateDeliveryType(Guid id, [FromBody] DeliveryTypeDto type)
    {
        try
        {
            return Ok(await this._deliveryTypeService.Update(id, type));
        }
        catch (Exception)
        {
            return BadRequest("Can't find delivery type");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDeliveryType(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._deliveryTypeRepository.Delete(id) });
        }
        catch (Exception)
        {
            return BadRequest("Can't find delivery type");
        }
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            this._deliveryTypeService.DeactivateDeliveryType(id);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Can't find delivery type");
        }
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        try
        {
            this._deliveryTypeService.ActivateDeliveryType(id);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Can't find delivery type");
        }
    }
}