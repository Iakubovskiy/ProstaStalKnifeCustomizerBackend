using System.Data.Entity.Core;
using API.Components.Sheaths.Presenter;
using Application.Components.Activate;
using Application.Components.Deactivate;
using Application.Components.SimpleComponents.Sheaths;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Domain.Component.Sheaths;
using Infrastructure.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Components.Sheaths;

[Route("api/sheaths")]
[ApiController]
public class SheathController: ControllerBase
{
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly IActivate<Sheath> _activateService;
    private readonly IDeactivate<Sheath> _deactivateService;
    private readonly ICreateService<Sheath, SheathDto> _sheathCreateService;
    private readonly IUpdateService<Sheath, SheathDto> _sheathUpdateService;
    private readonly SheathPresenter _sheathPresenter;
    
    public SheathController (
        IComponentRepository<Sheath> sheathRepository,
        IActivate<Sheath> activateService,
        IDeactivate<Sheath> deactivateService,
        ICreateService<Sheath, SheathDto> sheathCreateService,
        IUpdateService<Sheath, SheathDto> sheathUpdateService,
        SheathPresenter sheathPresenter
    )
    {
        this._sheathRepository = sheathRepository;
        this._sheathCreateService = sheathCreateService;
        this._activateService = activateService;
        this._deactivateService = deactivateService;
        this._sheathUpdateService = sheathUpdateService;
        this._sheathPresenter = sheathPresenter;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSheaths(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await this._sheathPresenter.PresentList(await this._sheathRepository.GetAll(), locale, currency));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveSheaths(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await this._sheathPresenter.PresentList(await this._sheathRepository.GetAllActive(), locale, currency));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSheathsById(
        Guid id,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        try
        {
            return Ok(await this._sheathPresenter.Present(await this._sheathRepository.GetById(id), locale, currency));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSheath(
        [FromBody] SheathDto newColor
    )
    {
        return Created(nameof(this.GetSheathsById), await this._sheathCreateService.Create(newColor));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSheath (
        Guid id, 
        [FromBody] SheathDto newColor
    )
    {
        try
        {
            return Ok(await this._sheathUpdateService.Update(id, newColor));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSheath(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._sheathRepository.Delete(id) });
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