using System.Data.Entity.Core;
using Application.Components.Activate;
using Application.Components.Deactivate;
using Application.Components.SimpleComponents.Engravings;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Microsoft.AspNetCore.Mvc;
using Domain.Component.Engravings;
using Infrastructure.Components;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Components.Engravings;

[Route("api/engravings")]
[ApiController]
public class EngravingController : ControllerBase
{
    private readonly IComponentRepository<Engraving> _engravingRepository;
    private readonly ICreateService<Engraving, EngravingDto> _createEngravingService;
    private readonly IUpdateService<Engraving, EngravingDto> _updateEngravingService;
    private readonly IActivate<Engraving> _activateService;
    private readonly IDeactivate<Engraving> _deactivateService;

    public EngravingController(
        IComponentRepository<Engraving> engravingRepository,
        ICreateService<Engraving, EngravingDto> createEngravingService,
        IUpdateService<Engraving, EngravingDto> updateEngravingService,
        IActivate<Engraving> activateService,
        IDeactivate<Engraving> deactivateService
    )
    {
        this._engravingRepository = engravingRepository;
        this._createEngravingService = createEngravingService;
        this._updateEngravingService = updateEngravingService;
        this._activateService = activateService;
        this._deactivateService = deactivateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEngravings()
    {
        return Ok(await this._engravingRepository.GetAll());
    }
    
    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveBladeShapes()
    {
        return Ok(await this._engravingRepository.GetAllActive());
    }

    [HttpGet ("{id:guid}")]
    public async Task<IActionResult> GetEngravingsById(Guid id)
    {
        try
        {
            return Ok( await this._engravingRepository.GetById(id));
        }
        catch (Exception)
        {
            return NotFound("Can't find engraving");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEngraving([FromBody] EngravingDto newEngraving)
    {
        return Created(nameof(this.GetEngravingsById), await this._createEngravingService.Create(newEngraving));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEngraving(Guid id, [FromBody] EngravingDto newEngraving)
    {
        try
        {
            return Ok(await this._updateEngravingService.Update(id, newEngraving));
        }
        catch (Exception)
        {
            return NotFound("Can't find engraving");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEngraving(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._engravingRepository.Delete(id) });
        }
        catch (Exception)
        {
            return NotFound("Can't find engraving");
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