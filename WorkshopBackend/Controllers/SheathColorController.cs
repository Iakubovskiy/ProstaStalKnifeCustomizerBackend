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

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveSheathColors()
        {
            return Ok(await _sheathColorService.GetAllActiveSheathColors());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSheathColorsById(Guid id)
        {
            return Ok(await _sheathColorService.GetSheathColorById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSheathColor(
            [FromForm] SheathColor newColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnesMap
            )
        {
            return Ok(await _sheathColorService.CreateSheathColor(
                newColor, 
                colorMap,
                normalMap,
                roughnesMap
             ));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSheathColor(
            Guid id, 
            [FromForm] SheathColor updatedColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnesMap
            )
        {
            return Ok(await _sheathColorService.UpdateSheathColor(id, updatedColor, colorMap,normalMap,roughnesMap));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSheathColor(Guid id)
        {
            return Ok(new { isDeleted = await _sheathColorService.DeleteSheathColor(id) });
        }

        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            return Ok(await _sheathColorService.ChangeActive(id, false));
        }

        [HttpPatch("activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            return Ok(await _sheathColorService.ChangeActive(id, true));
        }
    }
}
