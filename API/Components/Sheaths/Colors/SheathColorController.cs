using System.Data.Entity.Core;
using API.Components.Sheaths.Colors.Presenters;
using Application.Components.ComponentsWithType.SheathColors;
using Application.Components.ComponentsWithType.SheathColors.Activate;
using Application.Components.ComponentsWithType.UseCases.Create;
using Application.Components.ComponentsWithType.UseCases.Deactivate;
using Application.Components.ComponentsWithType.UseCases.Update;
using Application.Currencies;
using Microsoft.AspNetCore.Mvc;
using Domain.Component.Sheaths.Color;
using Infrastructure.Components.Sheaths.Color;

namespace API.Components.Sheaths.Colors;

[Route("api/sheath-colors")]
[ApiController]
public class SheathColorController : ControllerBase
{
    private readonly ISheathColorRepository _sheathColorRepository;
    private readonly ICreateTypeDependencyComponentService<SheathColor, SheathColorDto> _sheathColorCreateService;
    private readonly IUpdateTypeDependencyComponentService<SheathColor, SheathColorDto> _sheathColorUpdateService;
    private readonly IActivateSheathColorService _activateSheathColorService;
    private readonly IDeactivateSheathColorService _deactivateSheathColorService;
    private readonly IPriceService _priceService;

    public SheathColorController(
        ISheathColorRepository sheathColorRepository,
        ICreateTypeDependencyComponentService<SheathColor, SheathColorDto> sheathColorCreateService,
        IUpdateTypeDependencyComponentService<SheathColor, SheathColorDto> sheathColorUpdateService,
        IActivateSheathColorService activateSheathColorService,
        IDeactivateSheathColorService deactivateSheathColorService,
        IPriceService priceService
    )
    {
        this._sheathColorRepository = sheathColorRepository;
        this._sheathColorCreateService = sheathColorCreateService;
        this._sheathColorUpdateService = sheathColorUpdateService;
        this._activateSheathColorService = activateSheathColorService;
        this._deactivateSheathColorService = deactivateSheathColorService;
        this._priceService = priceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSheathColors(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await SheathColorPresenter
            .PresentList(await this._sheathColorRepository.GetAll(), locale, currency, this._priceService));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveSheathColors(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await SheathColorPresenter
            .PresentList(await this._sheathColorRepository.GetAllActive(), locale, currency, this._priceService));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSheathColorsById(
        Guid id,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        try
        {
            return Ok(await SheathColorPresenter
                .PresentWithTranslations(await this._sheathColorRepository.GetById(id), locale, currency, this._priceService));
        }
        catch (ObjectNotFoundException)
        {
            return NotFound("Can't find sheath color");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSheathColor(
        [FromBody] SheathColorDto newColor,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Created(nameof(this.GetSheathColorsById), await SheathColorPresenter
            .PresentWithTranslations(await this._sheathColorCreateService.Create(newColor),
                locale, currency, this._priceService
        ));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSheathColor(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency,
        Guid id, 
        [FromBody] SheathColorDto updatedColor
    )
    {
        try
        {
            return Ok(await SheathColorPresenter.Present(
                await this._sheathColorUpdateService.Update(id, updatedColor),
                locale, currency, this._priceService
            ));
        }
        catch (Exception)
        {
            return NotFound("Can't find sheath color");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSheathColor(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._sheathColorRepository.Delete(id) });
        }
        catch (Exception)
        {
            return NotFound("Can't find sheath color");
        }
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            await this._deactivateSheathColorService.Deactivate(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find sheath color");
        }
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        try
        {
            await this._activateSheathColorService.Activate(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find sheath color");
        }
    }
}