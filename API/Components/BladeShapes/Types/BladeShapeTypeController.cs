using System.Data.Entity.Core;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Components.BladeShapes.Types;

[Route("api/blade-shape-types")]
[ApiController]
public class BladeShapeTypeController : ControllerBase
{
    private readonly IRepository<BladeShapeType> _bladeShapeTypeRepository;
    
    public BladeShapeTypeController(IRepository<BladeShapeType> bladeShapeTypeRepository)
    {
        this._bladeShapeTypeRepository = bladeShapeTypeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBladeShapeTypes()
    {
        return Ok(await this._bladeShapeTypeRepository.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBladeShapeTypeById(Guid id)
    {
        try
        {
            return Ok(await this._bladeShapeTypeRepository.GetById(id));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBladeShapeType([FromBody] BladeShapeType bladeShapeType)
    {
        return Created(nameof(this.GetBladeShapeTypeById), await this._bladeShapeTypeRepository.Create(bladeShapeType));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBladeShapeType(Guid id, [FromBody] BladeShapeType bladeShapeType)
    {
        return Ok(await this._bladeShapeTypeRepository.Update(id, bladeShapeType));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBladeShapeType(Guid id)
    {
        try
        {
            return Ok(await this._bladeShapeTypeRepository.Delete(id));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}