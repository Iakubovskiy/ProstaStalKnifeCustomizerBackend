using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application.Services;
using API.Presenters;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InitialController:ControllerBase
{
    private readonly BladeShapeService _bladeShapeService;
    private readonly BladeCoatingColorService _bladeCoatingColorService;
    private readonly SheathColorService _sheathColorService;
    private readonly HandleColorService _handleColorService;
    
    public InitialController(
        BladeShapeService bladeShapeService, 
        BladeCoatingColorService bladeCoatingColorService,
        SheathColorService sheathColorService,
        HandleColorService handleColorService
    ) {
            _bladeShapeService = bladeShapeService;
            _bladeCoatingColorService = bladeCoatingColorService;
            _sheathColorService = sheathColorService;
            _handleColorService = handleColorService;
    }
    
    [HttpGet]
    public async Task<IActionResult> InitialDataForFrontend()
    {
        BladeShape? shape;
        BladeCoatingColor? coatingColor;
        SheathColor? sheathColor;
        HandleColor? handleColor;
        try
        {
            shape = (await _bladeShapeService.GetAllActiveBladeShapes())[0];
        }
        catch (IndexOutOfRangeException)
        {
            shape = null;
        }
        try
        {
            coatingColor = (await _bladeCoatingColorService.GetAllActiveBladeCoatingColors())[0];
        }
        catch (IndexOutOfRangeException)
        {
            coatingColor = null;
        }
        try
        {
            handleColor = (await _handleColorService.GetAllActiveHandleColors())[0];
        }
        catch (IndexOutOfRangeException)
        {
            handleColor = null;
        }
        try
        {
            sheathColor = (await _sheathColorService.GetAllActiveSheathColors())[0];
        }
        catch (IndexOutOfRangeException)
        {
            sheathColor = null;
        }

        InitialDataPresenter initialData = new InitialDataPresenter();
        initialData.BladeCoatingColor = coatingColor;
        initialData.BladeShape = shape;
        initialData.HandleColor = handleColor;
        initialData.SheathColor = sheathColor;
        
        return Ok(initialData);
    }
}