using System.Data.Entity.Core;
using Application.Components.ComponentsWithType.SheathColors;
using Application.Components.ComponentsWithType.SheathColors.Activate;
using Application.Components.ComponentsWithType.UseCases.Create;
using Application.Components.ComponentsWithType.UseCases.Deactivate;
using Application.Components.ComponentsWithType.UseCases.Get;
using Application.Components.ComponentsWithType.UseCases.Update;
using Application.Components.SimpleComponents.UseCases;
using Microsoft.AspNetCore.Mvc;
using Domain.Component.Sheaths.Color;
using Infrastructure.Components.Sheaths.Color;

namespace API.Components.Sheaths.Colors;

[Route("api/sheath-colors")]
[ApiController]
public class SheathColorController : ControllerBase
{
    private readonly ISheathColorViewService _sheathColorViewService;
    private readonly ISheathColorRepository _sheathColorRepository;
    private readonly ICreateTypeDependencyComponentService<SheathColor, SheathColorDto> _sheathColorCreateService;
    private readonly IUpdateTypeDependencyComponentService<SheathColor, SheathColorDto> _sheathColorUpdateService;
    private readonly IActivateSheathColorService _activateSheathColorService;
    private readonly IDeactivateSheathColorService _deactivateSheathColorService;
    private readonly ISheathColorMapper _sheathColorMapper;

    public SheathColorController(
        ISheathColorRepository sheathColorRepository,
        ISheathColorViewService sheathColorViewService,
        ICreateTypeDependencyComponentService<SheathColor, SheathColorDto> sheathColorCreateService,
        IUpdateTypeDependencyComponentService<SheathColor, SheathColorDto> sheathColorUpdateService,
        IActivateSheathColorService activateSheathColorService,
        IDeactivateSheathColorService deactivateSheathColorService,
        ISheathColorMapper sheathColorMapper
    )
    {
        this._sheathColorRepository = sheathColorRepository;
        this._sheathColorViewService = sheathColorViewService;
        this._sheathColorCreateService = sheathColorCreateService;
        this._sheathColorUpdateService = sheathColorUpdateService;
        this._activateSheathColorService = activateSheathColorService;
        this._deactivateSheathColorService = deactivateSheathColorService;
        this._sheathColorMapper = sheathColorMapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSheathColors()
    {
        return Ok(await this._sheathColorViewService.GetAllAsync());
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveSheathColors()
    {
        return Ok(await this._sheathColorViewService.GetAllActiveAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSheathColorsById(Guid id)
    {
        try
        {
            return Ok(await this._sheathColorViewService.GetByIdAsync(id));
        }
        catch (ObjectNotFoundException)
        {
            return NotFound("Can't find sheath color");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSheathColor(
        [FromBody] SheathColorDto newColor
    )
    {
        SheathColorResponsePresenter presenter = await this._sheathColorMapper.MapToPresenter(await this._sheathColorCreateService.Create(
            newColor
        ));
        return Created(nameof(this.GetSheathColorsById), presenter);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSheathColor(
        Guid id, 
        [FromBody] SheathColorDto updatedColor
    )
    {
        try
        {
            return Ok(this._sheathColorMapper.MapToPresenter(await this._sheathColorUpdateService.Update(id, updatedColor)));
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