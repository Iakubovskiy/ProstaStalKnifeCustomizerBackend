using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.DTO;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngravingController : ControllerBase
    {
        private readonly EngravingService _engravingService;

        public EngravingController(EngravingService service)
        {
            _engravingService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEngravings()
        {
            return Ok(new { engravings = await _engravingService.GetAllEngravings() });
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetEngravingsById(int id)
        {
            return Ok(new { engraving = await _engravingService.GetEngravingById(id) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateEngraving([FromForm] Engraving engraving, IFormFile? engravingPicrutre)
        {
            return Ok(new { createdEngraving = await _engravingService.CreateEngraving(engraving, engravingPicrutre) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEngraving(int id, [FromForm] Engraving engraving, IFormFile? engravingPicrutre)
        {
            return Ok(new { updatedEngraving = await _engravingService.UpdateEngraving(id, engraving, engravingPicrutre) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngraving(int id)
        {
            return Ok(new { isDeleted = await _engravingService.DeleteEngraving(id) });
        }
    }
}
