using Domain.Component.BladeShapeTypes;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BladeShapeTypeController : ControllerBase
{
    private readonly IRepository<BladeShapeType> _bladeShapeTypeRepository;
    
    public BladeShapeTypeController(IRepository<BladeShapeType> bladeShapeTypeRepository)
    {
        _bladeShapeTypeRepository = bladeShapeTypeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBladeShapeTypes()
    {
        return Ok(await this._bladeShapeTypeRepository.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBladeShapeTypeById(Guid id)
    {
        return Ok(await this._bladeShapeTypeRepository.GetById(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBladeShapeType([FromBody] BladeShapeType bladeShapeType)
    {
        return Ok(await this._bladeShapeTypeRepository.Create(bladeShapeType));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBladeShapeType(Guid id, [FromBody] BladeShapeType bladeShapeType)
    {
        return Ok(await this._bladeShapeTypeRepository.Update(id, bladeShapeType));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBladeShapeType(Guid id)
    {
        return Ok(await this._bladeShapeTypeRepository.Delete(id));
    }
}