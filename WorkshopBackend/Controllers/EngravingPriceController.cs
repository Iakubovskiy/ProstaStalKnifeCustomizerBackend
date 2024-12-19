using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
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
            return Ok(new { engravingPrices = await _engravingPriceService.GetAllEngravingPrices() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEngravingPricesById(int id)
        {
            return Ok(new { price = await _engravingPriceService.GetEngravingPriceById(id) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateEngravingPrice([FromForm] EngravingPrice price)
        {
            return Ok(new { createdPrice = await _engravingPriceService.CreateEngravingPrice(price) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEngravingPrice(int id, [FromForm] EngravingPrice price)
        {
            return Ok(new { updatedPrice = await _engravingPriceService.UpdateEngravingPrice(id, price) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngravingPrice(int id)
        {
            return Ok(new { isDeleted = await _engravingPriceService.DeleteEngravingPrice(id) });
        }
    }
}
