using Application.Components.Prices.Engravings;
using Microsoft.AspNetCore.Mvc;
using Domain.Component.Engravings.Support;
using Infrastructure;

namespace API.Components.Engravings.Support.Price;

[Route("api/engraving-prices")]
[ApiController]
public class EngravingPriceController : ControllerBase
{
    private readonly IRepository<EngravingPrice> _engravingPriceRepository;
    private readonly IGetEngravingPrice _getEngravingPriceService;

    public EngravingPriceController(
        IRepository<EngravingPrice> engravingPriceRepository,
        IGetEngravingPrice getEngravingPriceService
    )
    {
        this._engravingPriceRepository = engravingPriceRepository;
        this._getEngravingPriceService = getEngravingPriceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEngravingPrice([FromHeader(Name = "Currency")] string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            return BadRequest("Currency not set in Headers");

        try
        {
            var price = await this._getEngravingPriceService.GetPrice(currency);
            return Ok(price);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEngravingPrice([FromBody] EngravingPrice engravingPrice)
    {
        return Created(nameof(GetEngravingPrice), await this._engravingPriceRepository.Create(engravingPrice));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEngravingPrice(Guid id, [FromBody] EngravingPrice engravingPrice)
    {
        try
        {
            return Ok(await this._engravingPriceRepository.Update(id, engravingPrice));
        }
        catch (Exception)
        {
            return NotFound("Can't find engraving price");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEngravingPrice(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._engravingPriceRepository.Delete(id) });
        }
        catch (Exception)
        {
            return NotFound("Can't find engraving price");
        }
    }
}