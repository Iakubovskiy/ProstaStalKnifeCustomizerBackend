using API.Components.Products.Knives.Presenter;
using Microsoft.AspNetCore.Mvc;
using API.Presenters;
using Application.Components.Prices;
using Application.Currencies;
using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.Handles;
using Domain.Component.Product.Knife;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;
using Infrastructure.Components;
using Infrastructure.Components.Sheaths.Color;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InitialController:ControllerBase
{
    private readonly IComponentRepository<Knife> _knifeRepository;
    private readonly IGetComponentPrice _getComponentPrice;
    private readonly IPriceService _priceService;

    public InitialController(
        IComponentRepository<Knife> knifeRepository,
        IGetComponentPrice getComponentPrice,
        IPriceService priceService
    )
    {
        this._knifeRepository = knifeRepository;
        this._getComponentPrice = getComponentPrice;
        this._priceService = priceService;
    }

    [HttpGet]
    public async Task<IActionResult> InitialDataForFrontend(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        var knives = await this._knifeRepository.GetAllActive();
        if (knives.Count < 1)
        {
            return BadRequest("There are no knifes.");
        }
        return Ok(KnifePresenter
            .PresentForCanvas(knives[0], locale, currency, this._getComponentPrice, this._priceService));
    }
}