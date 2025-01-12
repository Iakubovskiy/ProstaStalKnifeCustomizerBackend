using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveBladeShapes()
        {
            return Ok(await _bladeShapeService.GetAllActiveBladeShapes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBladeShapesById(Guid id)
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
                Guid id, 
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
        public async Task<IActionResult> DeleteBladeShape(Guid id)
        {
            return Ok(new { isDeleted = await _bladeShapeService.DeleteBladeShape(id) });
        }

        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var bladeShape = await _bladeShapeService.ChangeActive(id, false);

            if (bladeShape == null)
            {
                return BadRequest(new { message = "Invalid BladeShape ID." });
            }

            return Ok(bladeShape);
        }

        [HttpPatch("activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var bladeShape = await _bladeShapeService.ChangeActive(id, true);

            if (bladeShape == null)
            {
                return BadRequest(new { message = "Invalid BladeShape ID." });
            }

            return Ok(bladeShape);
        }
    }
}
