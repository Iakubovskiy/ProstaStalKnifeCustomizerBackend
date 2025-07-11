using API.Orders.Support.PaymentMethods.Presenters;
using Application.Orders.Support.PaymentMethods;
using Application.Orders.Support.PaymentMethods.Data;
using Infrastructure.Orders.Support.PaymentMethods;
using Microsoft.AspNetCore.Mvc;

namespace API.Orders.Support.PaymentMethods;

[Route("api/payment-methods")]
[ApiController]
public class PaymentMethodController : ControllerBase
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentMethodController(
        IPaymentMethodRepository paymentMethodRepository,
        IPaymentMethodService paymentMethodService
    )
    {
        this._paymentMethodRepository = paymentMethodRepository;
        this._paymentMethodService = paymentMethodService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPaymentMethods([FromHeader(Name = "Locale")] string locale)
    {
        return Ok(await PaymentMethodPresenter.PresentList(await this._paymentMethodRepository.GetAll(), locale));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActivePaymentMethods([FromHeader(Name = "Locale")] string locale)
    {
        return Ok(await PaymentMethodPresenter.PresentList(await this._paymentMethodRepository.GetAllActive(), locale));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPaymentMethodsById(Guid id, [FromHeader(Name = "Locale")] string locale)
    {
        try
        {
            return Ok(await PaymentMethodPresenter.PresentWithTranslations(await this._paymentMethodRepository.GetById(id), locale));
        }
        catch (Exception)
        {
            return NotFound("Can't find PaymentMethod");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePaymentMethod([FromBody] PaymentMethodDto type)
    {
        return Created(nameof(GetPaymentMethodsById), await this._paymentMethodService.Create(type));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePaymentMethod(Guid id, [FromBody] PaymentMethodDto type)
    {
        try
        {
            return Ok(await this._paymentMethodService.Update(id, type));
        }
        catch (Exception)
        {
            return NotFound("Can't find delivery type");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePaymentMethod(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._paymentMethodRepository.Delete(id) });
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
            await this._paymentMethodService.DeactivatePaymentMethod(id);
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
            await this._paymentMethodService.ActivatePaymentMethod(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find delivery type");
        }
    }
}