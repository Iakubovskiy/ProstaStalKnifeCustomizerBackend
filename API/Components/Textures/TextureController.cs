using System.Data.Entity.Core;
using Application.Components.SimpleComponents.Textures;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Domain.Component.Textures;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Components.Textures;

[Route("api/textures")]
[ApiController]
public class TextureController : ControllerBase
{
    private readonly IRepository<Texture> _textureRepository;
    private readonly ISimpleCreateService<Texture, TextureDto> _createService;
    private readonly ISimpleUpdateService<Texture, TextureDto> _updateService;
    
    public TextureController(
        IRepository<Texture> textureRepository,
        ISimpleCreateService<Texture, TextureDto> createService,
        ISimpleUpdateService<Texture, TextureDto> updateService
    )
    {
        this._textureRepository = textureRepository;
        this._createService = createService;
        this._updateService = updateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await this._textureRepository.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            return Ok(await this._textureRepository.GetById(id));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TextureDto newTexture)
    {
        return Created(nameof(GetById), await this._createService.Create(newTexture));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TextureDto newTexture)
    {
        return Ok(await this._updateService.Update(id, newTexture));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            return Ok(await this._textureRepository.Delete(id));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}