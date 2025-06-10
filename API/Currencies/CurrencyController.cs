using System.Data.Entity.Core;
using Domain.Currencies;
using Infrastructure.Currencies;
using Microsoft.AspNetCore.Mvc;

namespace API.Currencies;

[Route("api/currencies")]
[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyRepository _currencyRepository;
    
    public CurrencyController(ICurrencyRepository currencyRepository)
    {
        this._currencyRepository = currencyRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await this._currencyRepository.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            return Ok(await this._currencyRepository.GetById(id));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Currency newCurrency)
    {
        return Created(nameof(GetById), await this._currencyRepository.Create(newCurrency));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Currency newCurrency)
    {
        try
        {
            return Ok(await this._currencyRepository.Update(id, newCurrency));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            return Ok(await this._currencyRepository.Delete(id));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}