using Microsoft.AspNetCore.Mvc;
using API.Presenters;
using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.Handles;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;
using Infrastructure.Components;
using Infrastructure.Components.Sheaths.Color;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InitialController:ControllerBase
{
    private readonly IComponentRepository<BladeShape> _bladeShapeRepository;
    private readonly IComponentRepository<BladeCoatingColor> _bladeCoatingColorRepository;
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly ISheathColorRepository _sheathColorRepository;
    private readonly IComponentRepository<Handle> _handleColorRepository;

    public InitialController(IComponentRepository<BladeShape> bladeShapeRepository, IComponentRepository<BladeCoatingColor> bladeCoatingColorRepository, IComponentRepository<Sheath> sheathRepository, ISheathColorRepository sheathColorRepository, IComponentRepository<Handle> handleColorRepository)
    {
        this._bladeShapeRepository = bladeShapeRepository;
        this._bladeCoatingColorRepository = bladeCoatingColorRepository;
        this._sheathRepository = sheathRepository;
        this._sheathColorRepository = sheathColorRepository;
        this._handleColorRepository = handleColorRepository;
    }

    [HttpGet]
    public async Task<IActionResult> InitialDataForFrontend()
    {
        BladeShape? shape;
        BladeCoatingColor? coatingColor;
        Sheath? sheath;
        SheathColor? sheathColor;
        Handle? handleColor;
        try
        {
            shape = (await this._bladeShapeRepository.GetAllActive())[0];
        }
        catch (IndexOutOfRangeException)
        {
            shape = null;
        }
        try
        {
            coatingColor = (await this._bladeCoatingColorRepository.GetAllActive())[0];
        }
        catch (IndexOutOfRangeException)
        {
            coatingColor = null;
        }
        try
        {
            handleColor = (await this._handleColorRepository.GetAllActive())[0];
        }
        catch (IndexOutOfRangeException)
        {
            handleColor = null;
        }
        try
        {
            sheath = (await this._sheathRepository.GetAllActive())[0];
        }
        catch (IndexOutOfRangeException)
        {
            sheath = null;
        }
        try
        {
            sheathColor = (await this._sheathColorRepository.GetAllActive())[0];
        }
        catch (IndexOutOfRangeException)
        {
            sheathColor = null;
        }

        InitialDataPresenter initialData = new InitialDataPresenter(
            shape,
            coatingColor,
            handleColor,
            sheath,
            sheathColor
        );
        
        return Ok(initialData);
    }
}