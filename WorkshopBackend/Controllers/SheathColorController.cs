using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.DTO;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheathColorController : ControllerBase
    {
        private readonly SheathColorService _sheathColorService;

        public SheathColorController(SheathColorService service)
        {
            _sheathColorService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSheathColors()
        {
            return Ok(new { sheathColors = await _sheathColorService.GetAllSheathColors() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSheathColorsById(int id)
        {
            return Ok(new { color = await _sheathColorService.GetSheathColorById(id) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSheathColor([FromForm] SheathColor color, IFormFile material)
        {
            return Ok(new { createdColor = await _sheathColorService.CreateSheathColor(color, material) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSheathColor(int id, [FromForm] SheathColor color, IFormFile? material)
        {
            return Ok(new { updatedColor = await _sheathColorService.UpdateSheathColor(id, color, material) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSheathColor(int id)
        {
            return Ok(new { isDeleted = await _sheathColorService.DeleteSheathColor(id) });
        }
    }
}
