using Application.Components.Activate;
using Application.Components.Deactivate;
using Application.Components.TexturedComponents.Data.Dto.BladeCoatings;
using Application.Components.TexturedComponents.UseCases.Create;
using Application.Components.TexturedComponents.UseCases.Update;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Component.BladeCoatingColors;
using Infrastructure;
using Infrastructure.Components;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BladeCoatingColorController : ControllerBase
{
    private readonly IComponentRepository<BladeCoatingColor> _bladeCoatingColorRepository;
    private readonly IActivate<BladeCoatingColor> _activateService;
    private readonly IDeactivate<BladeCoatingColor> _deactivateService;
    private readonly ICreateTexturedComponent<BladeCoatingColor, BladeCoatingDto> _bladeCoatingColorCreateService;
    private readonly IUpdateTexturedComponent<BladeCoatingColor, BladeCoatingDto> _bladeCoatingColorUpdateService;

    public BladeCoatingColorController (
        IComponentRepository<BladeCoatingColor> bladeCoatingColorRepository,
        IActivate<BladeCoatingColor> activateService,
        IDeactivate<BladeCoatingColor> deactivateService,
        ICreateTexturedComponent<BladeCoatingColor, BladeCoatingDto> bladeCoatingColorCreateService,
        IUpdateTexturedComponent<BladeCoatingColor, BladeCoatingDto> bladeCoatingColorUpdateService
    )
    {
        this._bladeCoatingColorRepository = bladeCoatingColorRepository;
        this._bladeCoatingColorCreateService = bladeCoatingColorCreateService;
        this._activateService = activateService;
        this._deactivateService = deactivateService;
        this._bladeCoatingColorUpdateService = bladeCoatingColorUpdateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBladeCoatingColors()
    {
        return Ok(await this._bladeCoatingColorRepository.GetAll());
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveBladeCoatingColors()
    {
        return Ok(await this._bladeCoatingColorRepository.GetAllActive());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBladeCoatingColorsById(Guid id)
    {
        return Ok(await this._bladeCoatingColorRepository.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBladeCoatingColor(
        [FromForm] BladeCoatingDto newColor
    )
    {
        return Ok(await this._bladeCoatingColorCreateService.Create(newColor));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBladeCoatingColor (
        Guid id, 
        [FromForm] BladeCoatingDto newColor
    )
    {
        return Ok(await this._bladeCoatingColorUpdateService.Update(id, newColor));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBladeCoatingColor(Guid id)
    {
        return Ok(new { isDeleted = await this._bladeCoatingColorRepository.Delete(id) });
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        BladeCoatingColor bladeCoatingColor = await this._bladeCoatingColorRepository.GetById(id);
        this._deactivateService.Deactivate(bladeCoatingColor);
        return Ok();
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        BladeCoatingColor bladeCoatingColor = await this._bladeCoatingColorRepository.GetById(id);
        this._activateService.Activate(bladeCoatingColor);
        return Ok();
    }
}