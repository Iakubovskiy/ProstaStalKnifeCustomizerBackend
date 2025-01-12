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
            return Ok(await _handleColorService.GetAllHandleColors());
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveHandleColors()
        {
            return Ok(await _handleColorService.GetAllActiveHandleColors());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHandleColorsById(Guid id)
        {
            return Ok(await _handleColorService.GetHandleColorById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateHandleColor(
            [FromForm] HandleColor handleColor,
             IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnesMap
            )
        {
            return Ok(await _handleColorService.CreateHandleColor(
                    handleColor, 
                    colorMap, 
                    normalMap, 
                    roughnesMap
                ) 
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHandleColor(
            Guid id, 
            [FromForm] HandleColor updateHandleColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnesMap
            )
        {
            return Ok(await _handleColorService.UpdateHandleColor(
                    id, 
                    updateHandleColor, 
                    colorMap,
                    normalMap, 
                    roughnesMap
                ) 
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHandleColor(Guid id)
        {
            return Ok(new { isDeleted = await _handleColorService.DeleteHandleColor(id) });
        }

        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            return Ok(await _handleColorService.ChangeActive(id, false));
        }

        [HttpPatch("activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            return Ok(await _handleColorService.ChangeActive(id, true));
        }
    }
}
