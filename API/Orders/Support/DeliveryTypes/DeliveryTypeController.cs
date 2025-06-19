using API.Orders.Support.DeliveryTypes.Presenters;
using Application.Currencies;
using Application.Orders.Support.DeliveryTypes;
using Application.Orders.Support.DeliveryTypes.Data;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Orders.Support.DeliveryTypes;

namespace API.Orders.Support.DeliveryTypes;

[Route("api/delivery-types")]
[ApiController]
public class DeliveryTypeController : ControllerBase
{
    private readonly IDeliveryTypeRepository _deliveryTypeRepository;
    private readonly IDeliveryTypeService _deliveryTypeService;
    private readonly IPriceService _priceService;

    public DeliveryTypeController(
        IDeliveryTypeRepository deliveryTypeRepository,
        IDeliveryTypeService deliveryTypeService,
        IPriceService priceService
    )
    {
        this._deliveryTypeRepository = deliveryTypeRepository;
        this._deliveryTypeService = deliveryTypeService;
        this._priceService = priceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDeliveryTypes(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await DeliveryTypePresenter.PresentList(
            await this._deliveryTypeRepository.GetAll(), 
            locale, 
            currency,
            this._priceService
        ));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveDeliveryTypes(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await DeliveryTypePresenter.PresentList(
            await this._deliveryTypeRepository.GetAllActive(), 
            locale, 
            currency,
            this._priceService
        ));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDeliveryTypesById(
        Guid id,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        try
        {
            return Ok(await DeliveryTypePresenter.PresentWithTranslations(
                await this._deliveryTypeRepository.GetById(id), 
                locale, 
                currency,
                this._priceService
            ));
        }
        catch (Exception)
        {
            return NotFound("Can't find DeliveryType");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateDeliveryType([FromBody] DeliveryTypeDto type)
    {
        return Created(nameof(GetDeliveryTypesById), await this._deliveryTypeService.Create(type));
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
            return NotFound("Can't find delivery type");
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
            return NotFound("Can't find delivery type");
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
            return NotFound("Can't find delivery type");
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
            return NotFound("Can't find delivery type");
        }
    }
}