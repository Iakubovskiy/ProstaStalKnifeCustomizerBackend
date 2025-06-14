﻿using System.Data.Entity.Core;
using Application.Components.Activate;
using Application.Components.Deactivate;
using Application.Components.TexturedComponents.Data.Dto.BladeCoatings;
using Application.Components.TexturedComponents.UseCases.Create;
using Application.Components.TexturedComponents.UseCases.Update;
using Microsoft.AspNetCore.Mvc;
using Domain.Component.BladeCoatingColors;
using Infrastructure.Components;

namespace API.Components.BladeCoatingColors;

[Route("api/blade-coating-colors")]
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
        try
        {
            return Ok(await this._bladeCoatingColorRepository.GetById(id));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBladeCoatingColor(
        [FromBody] BladeCoatingDto newColor
    )
    {
        return Created(nameof(GetBladeCoatingColorsById), await this._bladeCoatingColorCreateService.Create(newColor));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBladeCoatingColor (
        Guid id, 
        [FromBody] BladeCoatingDto newColor
    )
    {
        try
        {
            return Ok(await this._bladeCoatingColorUpdateService.Update(id, newColor));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBladeCoatingColor(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._bladeCoatingColorRepository.Delete(id) });
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            await this._deactivateService.Deactivate(id);
            return Ok();
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        try
        {
            await this._activateService.Activate(id);
            return Ok();
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}