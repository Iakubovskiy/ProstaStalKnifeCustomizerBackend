using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BladeCoatingColorController : ControllerBase
    {
        private readonly BladeCoatingColorService _bladeCoatingColorService;

        public BladeCoatingColorController ([FromForm]BladeCoatingColorService service)
        {
            _bladeCoatingColorService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBladeCoatingColors()
        {
            return Ok(new {bladeCoatingColors = await _bladeCoatingColorService.GetAllBladeCoatingColors()});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBladeCoatingColorsById(int id)
        {
            return Ok(new { color = await _bladeCoatingColorService.GetBladeCoatingColorById(id) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateBladeCoatingColor([FromForm] BladeCoatingColor color)
        {
            return Ok(new { createdColor = await _bladeCoatingColorService.CreateBladeCoatingColor(color) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBladeCoatingColor (int id, [FromForm] BladeCoatingColor color)
        {
            return Ok(new { updatedColor = await _bladeCoatingColorService.UpdateBladeCoatingColor(id, color) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBladeCoatingColor(int id)
        {
            return Ok(new { isDeleted = await _bladeCoatingColorService.DeleteBladeCoatingColor(id) });
        }
    }
}
