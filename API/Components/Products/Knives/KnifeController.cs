﻿using System.Data.Entity.Core;
using API.Components.Products.Knives.Presenter;
using Application.Components.Prices;
using Microsoft.AspNetCore.Mvc;
using Application.Components.Products.Knives;
using Application.Components.Products.UseCases.Activate;
using Application.Components.Products.UseCases.Create;
using Application.Components.Products.UseCases.Deactivate;
using Application.Components.Products.UseCases.Update;
using Application.Currencies;
using Domain.Component.Product.Knife;
using Infrastructure.Components;

namespace API.Components.Products.Knives;

[Route("api/knives")]
[ApiController]
public class KnifeController : ControllerBase
{
    private readonly ICreateProductService<Knife, KnifeDto> _createKnifeService;
    private readonly IUpdateProductService<Knife, KnifeDto> _updateProductService;
    private readonly IComponentRepository<Knife> _knifeRepository;
    private readonly IActivateProduct<Knife> _activateProductService;
    private readonly IDeactivateProduct<Knife> _deactivateProductService;
    private readonly IGetComponentPrice _getComponentPrice;
    private readonly IPriceService _priceService;

    public KnifeController(
        ICreateProductService<Knife, KnifeDto> createProductService,
        IUpdateProductService<Knife, KnifeDto> updateProductService,
        IComponentRepository<Knife> knifeRepository,
        IActivateProduct<Knife> activateProductService,
        IDeactivateProduct<Knife> deactivateProductService,
        IGetComponentPrice getComponentPrice,
        IPriceService priceService
    )
    {
        this._createKnifeService = createProductService;
        this._updateProductService = updateProductService;
        this._knifeRepository = knifeRepository;
        this._activateProductService = activateProductService;
        this._deactivateProductService = deactivateProductService;
        this._getComponentPrice = getComponentPrice;
        this._priceService = priceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllKnifes(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await KnifePresenter
            .PresentList(await this._knifeRepository
                .GetAll(), locale, currency, this._getComponentPrice, this._priceService));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveKnifes(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await KnifePresenter
            .PresentList(await this._knifeRepository
                .GetAllActive(), locale, currency, this._getComponentPrice, this._priceService));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetKnifesById(
        Guid id,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        try
        {
            Knife knife = await this._knifeRepository.GetById(id);
            return Ok(await KnifePresenter
                .PresentForCanvas(knife, locale, currency, this._getComponentPrice, this._priceService));
        }
        catch (ObjectNotFoundException)
        {
            return NotFound("Can't find knife");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateKnife(
        [FromBody] KnifeDto knife,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        try
        {
            return Ok(await KnifePresenter
                .Present(
                    await this._createKnifeService.Create(knife),
                    locale,
                    currency,
                    this._getComponentPrice,
                    this._priceService
                ));
        }
        catch (ObjectNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateKnife(
        Guid id, 
        [FromBody] KnifeDto knifeDto,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        try
        {
            var updatedKnife = await this._updateProductService.Update(id, knifeDto);

            return Ok(await KnifePresenter.Present(
                updatedKnife,
                locale,
                currency,
                this._getComponentPrice,
                this._priceService
            ));
        }
        catch (ObjectNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteKnife(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._knifeRepository.Delete(id) });
        }
        catch (Exception)
        {
            return NotFound("Can't find knife");
        }
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            await this._deactivateProductService.Deactivate(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find knife");
        }
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        try
        {
            await this._activateProductService.Activate(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find knife");
        }
    }
}