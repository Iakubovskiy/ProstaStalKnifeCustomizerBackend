using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.DTO;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BladeShapeController : ControllerBase
    {
        private readonly BladeShapeService _bladeShapeService;

        public BladeShapeController(BladeShapeService service)
        {
            _bladeShapeService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBladeShapes()
        {
            return Ok(await _bladeShapeService.GetAllBladeShapes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBladeShapesById(int id)
        {
            return Ok(await _bladeShapeService.GetBladeShapeById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBladeShape(
                [FromForm] BladeShape shape, 
                IFormFile bladeShapeModel,
                IFormFile sheathModel
            )
        {
            return Ok(
                await _bladeShapeService.CreateBladeShape(
                    shape,
                    bladeShapeModel,  
                    sheathModel
                )
            );

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBladeShape(
                int id, 
                [FromForm] BladeShape updateBladeShape,
                IFormFile? bladeShapeModel,
                IFormFile? sheathModel
            )
        {
            return Ok(
                await _bladeShapeService.UpdateBladeShape(
                    id,
                    updateBladeShape, 
                    bladeShapeModel, 
                    sheathModel
                ) 
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBladeShape(int id)
        {
            return Ok(new { isDeleted = await _bladeShapeService.DeleteBladeShape(id) });
        }
    }
}
