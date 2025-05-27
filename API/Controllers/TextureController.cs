using Application.Components.SimpleComponents.Textures;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Domain.Component.Textures;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TextureController : ControllerBase
{
    private readonly IRepository<Texture> _textureRepository;
    private readonly ICreateService<Texture, TextureDto> _createService;
    private readonly IUpdateService<Texture, TextureDto> _updateService;
    
    public TextureController(IRepository<Texture> textureRepository)
    {
        this._textureRepository = textureRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await this._textureRepository.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await this._textureRepository.GetById(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TextureDto newTexture)
    {
        return Ok(await this._createService.Create(newTexture));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TextureDto newTexture)
    {
        return Ok(await this._updateService.Update(id, newTexture));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await this._textureRepository.Delete(id));
    }
}