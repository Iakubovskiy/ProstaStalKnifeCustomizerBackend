using Application.Components.SimpleComponents.Engravings;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Microsoft.AspNetCore.Mvc;
using Domain.Component.Engravings;
using Infrastructure;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EngravingController : ControllerBase
{
    private readonly IRepository<Engraving> _engravingRepository;
    private readonly ICreateService<Engraving, EngravingDto> _createEngravingService;
    private readonly IUpdateService<Engraving, EngravingDto> _updateEngravingService;

    public EngravingController(
        IRepository<Engraving> engravingRepository,
        ICreateService<Engraving, EngravingDto> createEngravingService,
        IUpdateService<Engraving, EngravingDto> updateEngravingService
    )
    {
        this._engravingRepository = engravingRepository;
        this._createEngravingService = createEngravingService;
        this._updateEngravingService = updateEngravingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEngravings()
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
            return BadRequest("Can't find engraving");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEngraving([FromBody] EngravingDto newEngraving)
    {
        return Ok(await this._createEngravingService.Create(newEngraving));
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
            return BadRequest("Can't find engraving");
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
            return BadRequest("Can't find engraving");
        }
    }
}