using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.DTO;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FasteningController : ControllerBase
    {
        private readonly FasteningService _fasteningService;

        public FasteningController(FasteningService service)
        {
            _fasteningService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFastenings()
        {
            return Ok(await _fasteningService.GetAllFastenings());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFasteningsById(int id)
        {
            return Ok(await _fasteningService.GetFasteningById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFastening([FromForm] Fastening createFastening, IFormFile model)
        {
            return Ok(await _fasteningService.CreateFastening(
                    createFastening, 
                    model
                )
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFastening(int id, [FromForm] Fastening updateFastening, IFormFile? model)
        {
            return Ok(await _fasteningService.UpdateFastening(id, updateFastening, model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFastening(int id)
        {
            return Ok(new { isDeleted = await _fasteningService.DeleteFastening(id) });
        }
    }
}
