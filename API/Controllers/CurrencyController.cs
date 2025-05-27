using Domain.Currencies;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly IRepository<Currency> _currencyRepository;
    
    public CurrencyController(IRepository<Currency> currencyRepository)
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
        return Ok(await this._currencyRepository.GetById(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Currency newCurrency)
    {
        return Ok(await this._currencyRepository.Create(newCurrency));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Currency newCurrency)
    {
        return Ok(await this._currencyRepository.Update(id, newCurrency));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await this._currencyRepository.Delete(id));
    }
}