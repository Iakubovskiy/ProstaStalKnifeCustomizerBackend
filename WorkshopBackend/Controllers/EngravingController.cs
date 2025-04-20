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
            return Ok(await _engravingService.GetAllEngravings());
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetEngravingsById(Guid id)
        {
            try
            {
                return Ok( await _engravingService.GetEngravingById(id));
            }
            catch (Exception)
            {
                return BadRequest("Can't find engraving");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEngraving([FromForm] Engraving engraving, IFormFile? engravingPicrutre)
        {
            return Ok(await _engravingService.CreateEngraving(engraving, engravingPicrutre));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEngraving(Guid id, [FromForm] Engraving engraving, IFormFile? engravingPicrutre)
        {
            try
            {
                return Ok(await _engravingService.UpdateEngraving(id, engraving, engravingPicrutre));
            }
            catch (Exception)
            {
                return BadRequest("Can't find engraving");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngraving(Guid id)
        {
            try
            {
                return Ok(new { isDeleted = await _engravingService.DeleteEngraving(id) });
            }
            catch (Exception)
            {
                return BadRequest("Can't find engraving");
            }
        }
    }
}
