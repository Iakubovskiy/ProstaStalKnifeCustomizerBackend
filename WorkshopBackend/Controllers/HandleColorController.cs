using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.DTO;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandleColorController : ControllerBase
    {
        private readonly HandleColorService _handleColorService;

        public HandleColorController(HandleColorService service)
        {
            _handleColorService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHandleColors()
        {
            return Ok(new { handleColors = await _handleColorService.GetAllHandleColors() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHandleColorsById(int id)
        {
            return Ok(new { color = await _handleColorService.GetHandleColorById(id) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateHandleColor([FromForm] HandleColor handleColor, IFormFile material)
        {
            return Ok(new { createdColor = await _handleColorService.CreateHandleColor(
                    handleColor, 
                    material
                ) 
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHandleColor(int id, [FromForm] HandleColor updateHandleColor, IFormFile? material)
        {
            return Ok(new { updatedColor = await _handleColorService.UpdateHandleColor(
                    id, 
                    updateHandleColor, 
                    material
                ) 
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHandleColor(int id)
        {
            return Ok(new { isDeleted = await _handleColorService.DeleteHandleColor(id) });
        }
    }
}
