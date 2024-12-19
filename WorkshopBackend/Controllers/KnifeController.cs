using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnifeController : ControllerBase
    {
        private readonly KnifeService _knifeService;

        public KnifeController([FromForm] KnifeService service)
        {
            _knifeService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKnifes()
        {
            return Ok(new { knifes = await _knifeService.GetAllKnives() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKnifesById(int id)
        {
            return Ok(new { color = await _knifeService.GetKnifeById(id) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateKnife([FromForm] Knife color)
        {
            return Ok(new { createdColor = await _knifeService.CreateKnife(color) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKnife(int id, [FromForm] Knife color)
        {
            return Ok(new { updatedColor = await _knifeService.UpdateKnife(id, color) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnife(int id)
        {
            return Ok(new { isDeleted = await _knifeService.DeleteKnife(id) });
        }

        [HttpGet("price/{id}")]
        public async Task<IActionResult> GetKnifePrice(int id)
        {
            return Ok(new { price = await _knifeService.KnifePrice(id)});
        }
    }
}
