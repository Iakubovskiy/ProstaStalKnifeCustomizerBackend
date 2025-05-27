using Application.Components.Activate;
using Application.Components.Deactivate;
using Application.Components.SimpleComponents.Sheaths;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Domain.Component.Sheaths;
using Infrastructure.Components;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SheathController: ControllerBase
{
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly IActivate<Sheath> _activateService;
    private readonly IDeactivate<Sheath> _deactivateService;
    private readonly ICreateService<Sheath, SheathDto> _sheathCreateService;
    private readonly IUpdateService<Sheath, SheathDto> _sheathUpdateService;

    public SheathController (
        IComponentRepository<Sheath> sheathRepository,
        IActivate<Sheath> activateService,
        IDeactivate<Sheath> deactivateService,
        ICreateService<Sheath, SheathDto> sheathCreateService,
        IUpdateService<Sheath, SheathDto> sheathUpdateService
    )
    {
        this._sheathRepository = sheathRepository;
        this._sheathCreateService = sheathCreateService;
        this._activateService = activateService;
        this._deactivateService = deactivateService;
        this._sheathUpdateService = sheathUpdateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSheaths()
    {
        return Ok(await this._sheathRepository.GetAll());
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveSheaths()
    {
        return Ok(await this._sheathRepository.GetAllActive());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSheathsById(Guid id)
    {
        return Ok(await this._sheathRepository.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateSheath(
        [FromBody] SheathDto newColor
    )
    {
        return Ok(await this._sheathCreateService.Create(newColor));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSheath (
        Guid id, 
        [FromBody] SheathDto newColor
    )
    {
        return Ok(await this._sheathUpdateService.Update(id, newColor));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSheath(Guid id)
    {
        return Ok(new { isDeleted = await this._sheathRepository.Delete(id) });
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        Sheath sheath = await this._sheathRepository.GetById(id);
        this._deactivateService.Deactivate(sheath);
        return Ok();
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        Sheath sheath = await this._sheathRepository.GetById(id);
        this._activateService.Activate(sheath);
        return Ok();
    }
}