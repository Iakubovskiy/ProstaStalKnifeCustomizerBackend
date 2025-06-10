using Application.Components.SimpleComponents.Engravings.EngravingTags;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Domain.Component.Engravings.Support;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Components.Engravings.Support;

[Route("api/engraving-tags")]
[ApiController]
public class EngravingTagController : ControllerBase
{
    private readonly IRepository<EngravingTag> _engravingRepository;
    private readonly ISimpleCreateService<EngravingTag, EngravingTagDto> _simpleCreateEngravingService;
    private readonly ISimpleUpdateService<EngravingTag, EngravingTagDto> _updateEngravingTagService;
    
    public EngravingTagController(
        IRepository<EngravingTag> engravingRepository,
        ISimpleCreateService<EngravingTag, EngravingTagDto> simpleCreateEngravingService,
        ISimpleUpdateService<EngravingTag, EngravingTagDto> updateEngravingTagService
    )
    {
        this._engravingRepository = engravingRepository;
        this._simpleCreateEngravingService = simpleCreateEngravingService;
        this._updateEngravingTagService = updateEngravingTagService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEngravingTags()
    {
        return Ok(await this._engravingRepository.GetAll());
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
    public async Task<IActionResult> CreateEngravingTag([FromBody] EngravingTagDto newEngraving)
    {
        return Created(nameof(GetEngravingsById), await this._simpleCreateEngravingService.Create(newEngraving));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEngravingTag(Guid id, [FromBody] EngravingTagDto newEngraving)
    {
        try
        {
            return Ok(await this._updateEngravingTagService.Update(id, newEngraving));
        }
        catch (Exception)
        {
            return NotFound("Can't find engraving");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEngravingTag(Guid id)
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
}