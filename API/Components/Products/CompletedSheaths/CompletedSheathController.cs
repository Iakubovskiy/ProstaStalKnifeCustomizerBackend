using API.Components.Products.CompletedSheaths.Presenters;
using Application.Components.Prices;
using Application.Components.Products.CompletedSheaths;
using Application.Components.Products.UseCases.Activate;
using Application.Components.Products.UseCases.Create;
using Application.Components.Products.UseCases.Deactivate;
using Application.Components.Products.UseCases.Update;
using Application.Components.SimpleComponents.UseCases.Update;
using Application.Currencies;
using Domain.Component.Product.CompletedSheath;
using Infrastructure.Components;
using Microsoft.AspNetCore.Mvc;

namespace API.Components.Products.CompletedSheaths;

[ApiController]
[Route("api/products/completed-sheath")]
public class CompletedSheathController : ControllerBase
{
    private readonly IComponentRepository<CompletedSheath> _completedSheathRepository;
    private readonly IPriceService _priceService;
    private readonly IGetComponentPrice _getComponentPrice;
    private readonly ICreateProductService<CompletedSheath, CompletedSheathDto> _createCompletedSheathService;
    private readonly IUpdateProductService<CompletedSheath, CompletedSheathDto> _updateCompletedSheathService;
    private readonly IActivateProduct<CompletedSheath> _activateCompletedSheath;
    private readonly IDeactivateProduct<CompletedSheath> _deactivateCompletedSheath;

    public CompletedSheathController(
        IComponentRepository<CompletedSheath> completedSheathRepository,
        IGetComponentPrice getComponentPrice,
        IPriceService priceService,
        ICreateProductService<CompletedSheath, CompletedSheathDto> createCompletedSheathService,
        IUpdateProductService<CompletedSheath, CompletedSheathDto> updateCompletedSheathService,
        IActivateProduct<CompletedSheath> activateCompletedSheath,
        IDeactivateProduct<CompletedSheath> deactivateCompletedSheath
    )
    {
        this._completedSheathRepository = completedSheathRepository;
        this._priceService = priceService;
        this._getComponentPrice = getComponentPrice;
        this._createCompletedSheathService = createCompletedSheathService;
        this._updateCompletedSheathService = updateCompletedSheathService;
        this._activateCompletedSheath = activateCompletedSheath;
        this._deactivateCompletedSheath = deactivateCompletedSheath;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompletedSheaths(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await CompletedSheathPresenter.PresentList(
            await this._completedSheathRepository.GetAll(),
            locale,
            currency,
            this._getComponentPrice,
            this._priceService
        ));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveCompletedSheaths(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await CompletedSheathPresenter.PresentList(
            await this._completedSheathRepository.GetAllActive(),
            locale,
            currency,
            this._getComponentPrice,
            this._priceService
        ));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompletedSheathById(
        Guid id,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        try
        {
            var sheath = await this._completedSheathRepository.GetById(id);
            return Ok(await CompletedSheathPresenter.PresentWithTranslations(
                sheath, 
                locale, 
                currency, 
                this._getComponentPrice, 
                this._priceService
            ));
        }
        catch (Exception)
        {
            return NotFound("Can't find completed sheath");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompletedSheath([FromBody] CompletedSheathDto sheathDto)
    {
        return Ok(await this._createCompletedSheathService.Create(sheathDto));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCompletedSheath(Guid id, [FromBody] CompletedSheathDto sheathDto)
    {
        try
        {
            var updatedSheath = await this._updateCompletedSheathService.Update(id, sheathDto);
            return Ok(updatedSheath);
        }
        catch (Exception)
        {
            return NotFound("Can't find completed sheath");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompletedSheath(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._completedSheathRepository.Delete(id) });
        }
        catch (Exception)
        {
            return NotFound("Can't find completed sheath");
        }
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            await this._deactivateCompletedSheath.Deactivate(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find completed sheath");
        }
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        try
        {
            await this._activateCompletedSheath.Activate(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find completed sheath");
        }
    }
}