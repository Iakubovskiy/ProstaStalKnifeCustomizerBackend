using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application.Services;

namespace API.Controllers;

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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetHandleColorsById(Guid id)
    {
        try
        {
            return Ok(await _handleColorService.GetHandleColorById(id));
        }
        catch (Exception)
        {
            return BadRequest("Can't find handel color");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateHandleColor(
        [FromForm] HandleColor handleColor,
        IFormFile? colorMap,
        IFormFile? normalMap,
        IFormFile? roughnessMap
    )
    {
        return Ok(await _handleColorService.CreateHandleColor(
                handleColor, 
                colorMap, 
                normalMap, 
                roughnessMap
            ) 
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateHandleColor(
        Guid id, 
        [FromForm] HandleColor updateHandleColor,
        IFormFile? colorMap,
        IFormFile? normalMap,
        IFormFile? roughnessMap
    )
    {
        try
        {
            return Ok(await _handleColorService.UpdateHandleColor(
                    id, 
                    updateHandleColor, 
                    colorMap,
                    normalMap, 
                    roughnessMap
                ) 
            );
        }
        catch (Exception)
        {
            return BadRequest("Can't find handel color");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteHandleColor(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await _handleColorService.DeleteHandleColor(id) });
        }
        catch (Exception)
        {
            return BadRequest("Can't find handel color");
        }
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            return Ok(await _handleColorService.ChangeActive(id, false));
        }
        catch (Exception)
        {
            return BadRequest("Can't find handel color");
        }
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        try
        {
            return Ok(await _handleColorService.ChangeActive(id, true));
        }
        catch (Exception)
        {
            return BadRequest("Can't find handel color");
        }
    }
}