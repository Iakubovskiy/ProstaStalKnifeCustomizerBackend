using System.Data.Entity.Core;
using API.Components.BladeShapes.Presenters;
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
    private readonly BladeShapePresenter _bladeShapePresenter;

    public BladeShapeController(
        IComponentRepository<BladeShape> bladeShapeRepository, 
        IActivate<BladeShape> activateService,
        IDeactivate<BladeShape> deactivateService,
        ICreateService<BladeShape, BladeShapeDto> bladeShapeCreateService,
        IUpdateService<BladeShape, BladeShapeDto> bladeShapeUpdateService,
        BladeShapePresenter bladeShapePresenter
    )
    {
        this._bladeShapeRepository = bladeShapeRepository;
        this._activateService = activateService;
        this._deactivateService = deactivateService;
        this._bladeCoatingColorCreateService = bladeShapeCreateService;
        this._bladeCoatingColorUpdateService = bladeShapeUpdateService;
        this._bladeShapePresenter = bladeShapePresenter;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBladeShapes(
        [FromHeader(Name = "Locale")] string locale, [FromHeader(Name = "Currency")] string currency)
    {
        return Ok(await this._bladeShapePresenter.PresentList(await this._bladeShapeRepository.GetAll(), locale, currency));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveBladeShapes(
        [FromHeader(Name = "Locale")] string locale, [FromHeader(Name = "Currency")] string currency)
    {
        return Ok(await this._bladeShapePresenter.PresentList(await this._bladeShapeRepository.GetAllActive(), locale, currency));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBladeShapesById(
        Guid id,
        [FromHeader(Name = "Locale")] string locale, 
        [FromHeader(Name = "Currency")] string currency
    )
    {
        try
        {
            return Ok(await this._bladeShapePresenter.Present(await this._bladeShapeRepository.GetById(id), locale, currency));
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
        try
        {
            return Created(
                nameof(this.GetBladeShapesById),
                await this._bladeCoatingColorCreateService.Create(newShape)
            );
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

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
        try
        {
            return Ok(new { isDeleted = await this._bladeShapeRepository.Delete(id) });
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