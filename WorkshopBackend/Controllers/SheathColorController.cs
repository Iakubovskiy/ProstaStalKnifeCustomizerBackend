using Microsoft.AspNetCore.Mvc;
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
            try
            {
                return Ok(await _sheathColorService.GetSheathColorById(id));
            }
            catch (Exception)
            {
                return BadRequest("Can't find sheath color");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSheathColor(
            [FromForm] SheathColor newColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnessMap
            )
        {
            return Ok(await _sheathColorService.CreateSheathColor(
                newColor, 
                colorMap,
                normalMap,
                roughnessMap
             ));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSheathColor(
            Guid id, 
            [FromForm] SheathColor updatedColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnessMap
            )
        {
            try
            {
                return Ok(await _sheathColorService.UpdateSheathColor(id, updatedColor, colorMap,normalMap,roughnessMap));
            }
            catch (Exception)
            {
                return BadRequest("Can't find sheath color");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSheathColor(Guid id)
        {
            try
            {
                return Ok(new { isDeleted = await _sheathColorService.DeleteSheathColor(id) });
            }
            catch (Exception)
            {
                return BadRequest("Can't find sheath color");
            }
        }

        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            try
            {
                return Ok(await _sheathColorService.ChangeActive(id, false));
            }
            catch (Exception)
            {
                return BadRequest("Can't find sheath color");
            }
        }

        [HttpPatch("activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            try
            {
                return Ok(await _sheathColorService.ChangeActive(id, true));
            }
            catch (Exception)
            {
                return BadRequest("Can't find sheath color");
            }
        }
    }
}
