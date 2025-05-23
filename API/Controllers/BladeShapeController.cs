using Application.Components.Activate;
using Application.Components.Deactivate;
using Application.Components.SimpleComponents.BladeShapes;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Microsoft.AspNetCore.Mvc;
using Domain.Component.BladeShapes;
using Infrastructure.Components;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BladeShapeController : ControllerBase
{
    private readonly IComponentRepository<BladeShape> _bladeShapeRepository;
    private readonly IActivate<BladeShape> _activateService;
    private readonly IDeactivate<BladeShape> _deactivateService;
    private readonly ICreateSimpleComponent<BladeShape, BladeShapeDto> _bladeCoatingColorCreateService;
    private readonly IUpdateComponent<BladeShape, BladeShapeDto> _bladeCoatingColorUpdateService;

    public BladeShapeController(
        IComponentRepository<BladeShape> bladeShapeRepository, 
        IActivate<BladeShape> activateService,
        IDeactivate<BladeShape> deactivateService,
        ICreateSimpleComponent<BladeShape, BladeShapeDto> bladeShapeCreateService,
        IUpdateComponent<BladeShape, BladeShapeDto> bladeShapeUpdateService
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
        return Ok(await this._bladeShapeRepository.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBladeShape(
        [FromForm] BladeShapeDto newShape
    )
    {
        return Ok(
            await this._bladeCoatingColorCreateService.Create(newShape)
        );

    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBladeShape(
        Guid id, 
        [FromForm] BladeShapeDto newShape
    )
    {
        return Ok(
            await this._bladeCoatingColorUpdateService.Update(
                id,
                newShape
            ) 
        );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBladeShape(Guid id)
    {
        return Ok(new { isDeleted = await this._bladeShapeRepository.Delete(id) });
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        BladeShape bladeCoatingColor = await this._bladeShapeRepository.GetById(id);
        this._deactivateService.Deactivate(bladeCoatingColor);
        return Ok();
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        BladeShape bladeCoatingColor = await this._bladeShapeRepository.GetById(id);
        this._activateService.Activate(bladeCoatingColor);
        return Ok();
    }
}