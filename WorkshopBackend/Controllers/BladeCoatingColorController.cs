﻿using Microsoft.AspNetCore.Http;
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
            return Ok(await _bladeCoatingColorService.GetAllBladeCoatingColors());
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveBladeCoatingColors()
        {
            return Ok(await _bladeCoatingColorService.GetAllActiveBladeCoatingColors());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBladeCoatingColorsById(Guid id)
        {
            return Ok(await _bladeCoatingColorService.GetBladeCoatingColorById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBladeCoatingColor(
            [FromForm] BladeCoatingColor newColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnesMap
            )
        {
            return Ok(await _bladeCoatingColorService.CreateBladeCoatingColor(newColor,colorMap,normalMap,roughnesMap));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBladeCoatingColor (
            Guid id, 
            [FromForm] BladeCoatingColor newColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnesMap
            )
        {
            return Ok(await _bladeCoatingColorService.UpdateBladeCoatingColor(id, newColor, colorMap, normalMap, roughnesMap));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBladeCoatingColor(Guid id)
        {
            return Ok(new { isDeleted = await _bladeCoatingColorService.DeleteBladeCoatingColor(id) });
        }

        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            return Ok(await _bladeCoatingColorService.ChangeActive(id,false));
        }

        [HttpPatch("activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            return Ok(await _bladeCoatingColorService.ChangeActive(id, true));
        }
    }
}
