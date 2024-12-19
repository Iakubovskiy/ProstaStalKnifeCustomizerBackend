using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WorkshopBackend.DTO;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BladeCoatingController : ControllerBase
    {
        private readonly BladeCoatingService _bladeCoatingService;

        public BladeCoatingController(BladeCoatingService service)
        {
            _bladeCoatingService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBladeCoatings()
        {
            return Ok(await _bladeCoatingService.GetAllBladeCoatings());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBladeCoatingsById(int id)
        {
            return Ok(await _bladeCoatingService.GetBladeCoatingById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBladeCoating([FromForm] string coatingJson, IFormFile material)
        {
            var coating = JsonConvert.DeserializeObject<BladeCoating>(coatingJson);
            return Ok(await _bladeCoatingService.CreateBladeCoating(coating, material));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBladeCoating(int id, [FromForm] string coatingJson, IFormFile? material)
        {
            var coating = JsonConvert.DeserializeObject<BladeCoating>(coatingJson);
            return Ok(await _bladeCoatingService.UpdateBladeCoating(id, coating, material));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBladeCoating(int id)
        {
            return Ok(new { isDeleted = await _bladeCoatingService.DeleteBladeCoating(id) });
        }
    }
}
