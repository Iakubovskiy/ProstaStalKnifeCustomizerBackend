using System.Data.Entity.Core;
using Application.Components.Activate;
using Application.Components.Deactivate;
using Application.Components.SimpleComponents.BladeShapes;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Microsoft.AspNetCore.Mvc;
using Domain.Component.BladeShapes;
using Infrastructure.Components;

namespace API.Components.BladeShapes;

[Route("api/blade-shapes")]
[ApiController]
public class BladeShapeController : ControllerBase
{
    private readonly IComponentRepository<BladeShape> _bladeShapeRepository;
    private readonly IActivate<BladeShape> _activateService;
    private readonly IDeactivate<BladeShape> _deactivateService;
    private readonly ICreateService<BladeShape, BladeShapeDto> _bladeCoatingColorCreateService;
    private readonly IUpdateService<BladeShape, BladeShapeDto> _bladeCoatingColorUpdateService;

    public BladeShapeController(
        IComponentRepository<BladeShape> bladeShapeRepository, 
        IActivate<BladeShape> activateService,
        IDeactivate<BladeShape> deactivateService,
        ICreateService<BladeShape, BladeShapeDto> bladeShapeCreateService,
        IUpdateService<BladeShape, BladeShapeDto> bladeShapeUpdateService
    )
    {
        this._bladeShapeRepository = bladeShapeRepository;
        this._activateService = activateService;
        this._deactivateService = deactivateService;
        this._bladeCoatingColorCreateService = bladeShapeCreateService;
        this._bladeCoatingColorUpdateService = bladeShapeUpdateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBladeShapes()
    {
        return Ok(await this._bladeShapeRepository.GetAll());
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveBladeShapes()
    {
        return Ok(await this._bladeShapeRepository.GetAllActive());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBladeShapesById(Guid id)
    {
        try
        {
            return Ok(await this._bladeShapeRepository.GetById(id));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBladeShape(
        [FromBody] BladeShapeDto newShape
    )
    {
        return Created(
            nameof(this.GetBladeShapesById),
            await this._bladeCoatingColorCreateService.Create(newShape)
        );

    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBladeShape(
        Guid id, 
        [FromBody] BladeShapeDto newShape
    )
    {
        try
        {
            return Ok(
                await this._bladeCoatingColorUpdateService.Update(
                    id,
                    newShape
                ) 
            );
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBladeShape(Guid id)
    {
        return Ok(new { isDeleted = await this._bladeShapeRepository.Delete(id) });
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