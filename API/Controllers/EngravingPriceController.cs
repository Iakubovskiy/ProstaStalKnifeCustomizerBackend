using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application.Services;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EngravingPriceController : ControllerBase
{
    private readonly EngravingPriceService _engravingPriceService;

    public EngravingPriceController(EngravingPriceService service)
    {
        _engravingPriceService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEngravingPrices()
    {
        return Ok(await _engravingPriceService.GetAllEngravingPrices());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEngravingPricesById(Guid id)
    {
        try
        {
            return Ok(await _engravingPriceService.GetEngravingPriceById(id));
        }
        catch (Exception)
        {
            return BadRequest("Can't find engraving price");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEngravingPrice([FromForm] EngravingPrice engravingPrice)
    {
        return Ok(await _engravingPriceService.CreateEngravingPrice(engravingPrice));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEngravingPrice(Guid id, [FromForm] EngravingPrice engravingPrice)
    {
        try
        {
            return Ok(await _engravingPriceService.UpdateEngravingPrice(id, engravingPrice));
        }
        catch (Exception)
        {
            return BadRequest("Can't find engraving price");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEngravingPrice(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await _engravingPriceService.DeleteEngravingPrice(id) });
        }
        catch (Exception)
        {
            return BadRequest("Can't find engraving price");
        }
    }
}