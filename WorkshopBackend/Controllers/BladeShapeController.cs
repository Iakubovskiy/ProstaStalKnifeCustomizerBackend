using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveBladeShapes()
        {
            return Ok(await _bladeShapeService.GetAllActiveBladeShapes());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBladeShapesById(Guid id)
        {
            return Ok(await _bladeShapeService.GetBladeShapeById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBladeShape(
                [FromForm] BladeShape shape, 
                IFormFile bladeShapeModel,
                IFormFile sheathModel,
                IFormFile bladeShapePhoto
            )
        {
            return Ok(
                await _bladeShapeService.CreateBladeShape(
                    shape,
                    bladeShapeModel,  
                    sheathModel,
                    bladeShapePhoto
                )
            );

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBladeShape(
                Guid id, 
                [FromForm] BladeShape updateBladeShape,
                IFormFile? bladeShapeModel,
                IFormFile? sheathModel,
                IFormFile? bladeShapePhoto
            )
        {
            return Ok(
                await _bladeShapeService.UpdateBladeShape(
                    id,
                    updateBladeShape, 
                    bladeShapeModel, 
                    sheathModel,
                    bladeShapePhoto
                ) 
            );
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBladeShape(Guid id)
        {
            return Ok(new { isDeleted = await _bladeShapeService.DeleteBladeShape(id) });
        }

        [HttpPatch("deactivate/{id:guid}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            try
            {
                var bladeShape = await _bladeShapeService.ChangeActive(id, false);
                return Ok(bladeShape);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Invalid BladeShape ID." });
            }
        }

        [HttpPatch("activate/{id:guid}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            try
            {
                BladeShape bladeShape = await _bladeShapeService.ChangeActive(id, true);
                return Ok(bladeShape);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Invalid BladeShape ID." });
            }
        }
    }
}
