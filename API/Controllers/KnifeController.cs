using Microsoft.AspNetCore.Mvc;
using Application.Components.Products.Knives;
using Application.Components.Products.UseCases.Activate;
using Application.Components.Products.UseCases.Create;
using Application.Components.Products.UseCases.Deactivate;
using Application.Components.Products.UseCases.Update;
using Domain.Component.Product.Knife;
using Infrastructure.Components;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KnifeController : ControllerBase
{
    private readonly ICreateProductService<Knife, KnifeDto> _createKnifeService;
    private readonly IUpdateProductService<Knife, KnifeDto> _updateProductService;
    private readonly IComponentRepository<Knife> _knifeRepository;
    private readonly IActivateProduct<Knife> _activateProductService;
    private readonly IDeactivateProduct<Knife> _deactivateProductService;

    public KnifeController(
        ICreateProductService<Knife, KnifeDto> createProductService,
        IUpdateProductService<Knife, KnifeDto> updateProductService,
        IComponentRepository<Knife> knifeRepository,
        IActivateProduct<Knife> activateProductService,
        IDeactivateProduct<Knife> deactivateProductService
    )
    {
        this._createKnifeService = createProductService;
        this._updateProductService = updateProductService;
        this._knifeRepository = knifeRepository;
        this._activateProductService = activateProductService;
        this._deactivateProductService = deactivateProductService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllKnifes()
    {
        return Ok(await this._knifeRepository.GetAll());
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveKnifes()
    {
        return Ok(await this._knifeRepository.GetAllActive());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetKnifesById(Guid id)
    {
        try
        {
            return Ok(await this._knifeRepository.GetById(id));
        }
        catch (Exception)
        {
            return NotFound("Can't find knife");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateKnife([FromBody] KnifeDto knife)
    {
        return Ok(await this._createKnifeService.Create(knife));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateKnife(Guid id, [FromBody] KnifeDto knifeDto)
    {
        try
        {
            var updatedKnife = await this._updateProductService.Update(id, knifeDto);

            return Ok(updatedKnife);
        }
        catch (Exception)
        {
            return NotFound("Can't find knife");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteKnife(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._knifeRepository.Delete(id) });
        }
        catch (Exception)
        {
            return NotFound("Can't find knife");
        }
    }

    /*[HttpGet("price/{id:guid}")]
    public async Task<IActionResult> GetKnifePrice(Guid id)
    {
        try
        {
            return Ok(new { price = await _knifeService.KnifePrice(id)});
        }
        catch (Exception)
        {
            return BadRequest("Can't find knife");
        }
    }*/

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            this._deactivateProductService.Deactivate(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find knife");
        }
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        try
        {
            this._activateProductService.Activate(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find knife");
        }
    }
}