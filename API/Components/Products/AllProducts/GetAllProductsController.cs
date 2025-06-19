using System.Data.Entity.Core;
using API.Components.Products.AllProducts.Filters.Presenters;
using API.Components.Products.AllProducts.Presenters;
using Application.Components.Prices;
using Domain.Component.Product;
using Domain.Currencies;
using Infrastructure;
using Infrastructure.Components.Products;
using Infrastructure.Components.Products.Filters;
using Infrastructure.Components.Products.Filters.Characteristics;
using Infrastructure.Components.Products.Filters.Colors;
using Infrastructure.Components.Products.Filters.Price;
using Infrastructure.Components.Products.Filters.Styles;
using Infrastructure.Currencies;
using Microsoft.AspNetCore.Mvc;

namespace API.Components.Products.AllProducts;

[ApiController]
[Route("/api/products/catalog")]
public class GetAllProductsController : ControllerBase
{
    private const int PageSize = 20;
    private readonly IFilterStylesRepository _filterRepository;
    private readonly IGetBladeShapeCharacteristicsFilterRepository _getBladeShapeCharacteristicsFilterRepository;
    private readonly IColorsFilterRepository _colorsFilterRepository;
    private readonly IPriceFilterRepository _priceFilterRepository;
    private readonly IGetProductPaginatedList _getProductPaginatedList;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IGetComponentPrice _getComponentPrice;
    
    public GetAllProductsController(IFilterStylesRepository filterRepository,
        IGetBladeShapeCharacteristicsFilterRepository getBladeShapeCharacteristicsFilterRepository,
        IColorsFilterRepository colorsFilterRepository,
        IPriceFilterRepository priceFilterRepository,
        IGetProductPaginatedList getProductPaginatedList,
        ICurrencyRepository currencyRepository,
        IGetComponentPrice getComponentPrice
    )
    {
        this._filterRepository = filterRepository;
        this._getBladeShapeCharacteristicsFilterRepository = getBladeShapeCharacteristicsFilterRepository;
        this._colorsFilterRepository = colorsFilterRepository;
        this._priceFilterRepository = priceFilterRepository; 
        this._getProductPaginatedList = getProductPaginatedList;
        this._currencyRepository = currencyRepository;
        this._getComponentPrice = getComponentPrice;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency,
        [FromQuery] int page = 1,
        [FromQuery] string? productType = null,
        [FromQuery] List<string>? styles = null,
        [FromQuery] double? minBladeLength = null,
        [FromQuery] double? maxBladeLength = null,
        [FromQuery] double? minTotalLength = null,
        [FromQuery] double? maxTotalLength = null,
        [FromQuery] double? minBladeWidth = null,
        [FromQuery] double? maxBladeWidth = null,
        [FromQuery] double? minBladeWeight = null,
        [FromQuery] double? maxBladeWeight = null,
        [FromQuery] List<string>? colors = null,
        [FromQuery] double? minPrice = null,
        [FromQuery] double? maxPrice = null
    ) 
    {
        if (minPrice != null && maxPrice != null && minPrice > maxPrice)
        {
            return BadRequest("Min price cannot be greater than max price");
        }

        if (
            (
                minBladeLength != null || maxBladeLength != null
                || minTotalLength != null || maxTotalLength != null
                || minBladeWidth != null || maxBladeWidth != null
                || minBladeWeight != null || maxBladeWeight != null
            )
            &&
            productType != null && productType != "knife"
           )
        {
            return BadRequest("You cannot select blade characteristics for sheath or attachment");
        }

        Currency currencyFromDb;
        try
        {
            currencyFromDb = await this._currencyRepository.GetByName(currency);
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
        
        if(minPrice != null)
        {
            minPrice *= currencyFromDb.ExchangeRate;
        }

        if (maxPrice != null)
        {
            maxPrice *= currencyFromDb.ExchangeRate;
        }
        ProductFilters filters = new ProductFilters
        {
            ProductType = productType,
            Styles = styles,
            MinBladeLength = minBladeLength,
            MaxBladeLength = maxBladeLength,
            MinTotalLength = minTotalLength,
            MaxTotalLength = maxTotalLength,
            MinBladeWidth = minBladeWidth,
            MaxBladeWidth = maxBladeWidth,
            MinBladeWeight = minBladeWeight,
            MaxBladeWeight = maxBladeWeight,
            Colors = colors,
            MinPrice = minPrice,
            MaxPrice = maxPrice
        };

        #region getFilters

        List<string> stylesFilters = await this._filterRepository.GetAllStyleTags(locale);
        TextFilterPresenter stylePresenter = TextFilterPresenter.Present("style", stylesFilters);

        var bladeLength = await this._getBladeShapeCharacteristicsFilterRepository.GetBladeLengthFilters();
        NumericFilterPresenter bladeLengthPresenter = NumericFilterPresenter
            .Present("bladeLength", bladeLength.Item1, bladeLength.Item2);
        
        var totalLength = await this._getBladeShapeCharacteristicsFilterRepository.GetTotalLengthFilters();
        NumericFilterPresenter totalLengthPresent = NumericFilterPresenter
            .Present("totalLength", totalLength.Item1, totalLength.Item2);
        
        var bladeWidth = await this._getBladeShapeCharacteristicsFilterRepository.GetBladeWidthFilters();
        NumericFilterPresenter bladeWidthPresenter = NumericFilterPresenter
            .Present("bladeWidth", bladeWidth.Item1, bladeWidth.Item2);
        
        var bladeWeight = await this._getBladeShapeCharacteristicsFilterRepository.GetBladeWeightFilters();
        NumericFilterPresenter bladeWeightPresenter = NumericFilterPresenter
            .Present("bladeWeight", bladeWeight.Item1, bladeWeight.Item2);

        List<string> colorsFilters = await this._colorsFilterRepository.GetAllColorFilters(locale);
        TextFilterPresenter colorPresenter = TextFilterPresenter.Present("colors", colorsFilters);

        var prices = await this._priceFilterRepository.GetPriceFilter(currency);
        NumericFilterPresenter priceFilterPresenter = NumericFilterPresenter
            .Present("prices", prices.Item1, prices.Item2);

        #endregion

        PaginatedResult <Product> products = await this._getProductPaginatedList
            .GetProductPaginatedList(page, PageSize, locale, filters);
        
        return Ok(new {
            filters = new {
                stylePresenter,
                bladeLengthPresenter,
                totalLengthPresent,
                bladeWidthPresenter,
                bladeWeightPresenter,
                colorPresenter,
                priceFilterPresenter,
            },
            products = await ProductPresenter
                .PresentPaginatedList(products, locale, currency, this._getComponentPrice),
        });
    }
}