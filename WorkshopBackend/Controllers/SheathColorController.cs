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
            return Ok(await _sheathColorService.GetAllSheathColors());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSheathColorsById(int id)
        {
            return Ok(await _sheathColorService.GetSheathColorById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSheathColor([FromForm] SheathColor newColor, IFormFile handleMaterial)
        {
            return Ok(await _sheathColorService.CreateSheathColor(newColor, handleMaterial));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSheathColor(int id, [FromForm] SheathColor updatedColor, IFormFile? handleMaterial)
        {
            return Ok(await _sheathColorService.UpdateSheathColor(id, updatedColor, handleMaterial));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSheathColor(int id)
        {
            return Ok(new { isDeleted = await _sheathColorService.DeleteSheathColor(id) });
        }
    }
}
