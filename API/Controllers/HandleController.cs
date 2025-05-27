using Application.Components.Activate;
using Application.Components.Deactivate;
using Application.Components.TexturedComponents.Data.Dto.HandleColors;
using Application.Components.TexturedComponents.UseCases.Create;
using Application.Components.TexturedComponents.UseCases.Update;
using Microsoft.AspNetCore.Mvc;
using Domain.Component.Handles;
using Infrastructure.Components;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HandleController : ControllerBase
{
    private readonly IComponentRepository<Handle> _handleRepository;
    private readonly ICreateTexturedComponent<Handle, HandleColorDto> _handleCreateService;
    private readonly IUpdateTexturedComponent<Handle, HandleColorDto> _handleUpdateService;
    private readonly IActivate<Handle> _activateService;
    private readonly IDeactivate<Handle> _deactivateService;
    
    public HandleController(
        IComponentRepository<Handle> handleRepository,
        ICreateTexturedComponent<Handle, HandleColorDto> handleCreateService,
        IUpdateTexturedComponent<Handle, HandleColorDto> handleUpdateService,
        IActivate<Handle> activateService,
        IDeactivate<Handle> deactivateService
    )
    {
        this._handleRepository = handleRepository;
        this._handleCreateService = handleCreateService;
        this._handleUpdateService = handleUpdateService;
        this._activateService = activateService;
        this._deactivateService = deactivateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHandles()
    {
        return Ok(await this._handleRepository.GetAll());
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveHandles()
    {
        return Ok(await this._handleRepository.GetAllActive());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetHandleColorsById(Guid id)
    {
        try
        {
            return Ok(await this._handleRepository.GetById(id));
        }
        catch (Exception)
        {
            return BadRequest("Can't find handel color");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateHandleColor(
        [FromBody] HandleColorDto handleColor
    )
    {
        return Ok(await this._handleCreateService.Create(
                handleColor
            ) 
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateHandle(
        Guid id, 
        [FromBody] HandleColorDto newHandle
    )
    {
        try
        {
            return Ok(await this._handleUpdateService.Update(
                    id, 
                    newHandle
                ) 
            );
        }
        catch (Exception)
        {
            return BadRequest("Can't find handel color");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteHandleColor(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._handleRepository.Delete(id) });
        }
        catch (Exception)
        {
            return BadRequest("Can't find handel color");
        }
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            Handle handle = await this._handleRepository.GetById(id);
            this._deactivateService.Deactivate(handle);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Can't find handel color");
        }
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        try
        {
            Handle handle = await this._handleRepository.GetById(id);
            this._activateService.Activate(handle);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Can't find handel color");
        }
    }
}