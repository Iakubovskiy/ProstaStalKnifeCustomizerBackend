using Application.Currencies;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Sheaths.Color;

namespace API.Components.Sheaths.Colors.Presenters;

public class SheathColorPriceByTypePresenter
{
    private readonly IPriceService _priceService;

    public SheathColorPriceByTypePresenter(IPriceService priceService)
    {
        this._priceService = priceService;
    }

    public BladeShapeType BladeShapeType { get; set; }
    public double Price { get; set; }

    public async Task<SheathColorPriceByTypePresenter> Present(SheathColorPriceByType sheathColorPriceByType, string locale,
        string currency)
    {
        this.BladeShapeType = sheathColorPriceByType.Type;
        this.Price = await this._priceService.GetPrice(sheathColorPriceByType.Price, currency);
        
        return this;
    }

    public async Task<List<SheathColorPriceByTypePresenter>> PresentList(
        List<SheathColorPriceByType> sheathColorPriceByTypes, 
        string locale, 
        string currency
    )
    {
        List<SheathColorPriceByTypePresenter> sheathColorPriceByTypePresenters = new List<SheathColorPriceByTypePresenter>();
        foreach (SheathColorPriceByType colorPriceByType in sheathColorPriceByTypes)
        {
            SheathColorPriceByTypePresenter sheathColorPriceByType = new SheathColorPriceByTypePresenter(this._priceService);
            await sheathColorPriceByType.Present(colorPriceByType, locale, currency);
            sheathColorPriceByTypePresenters.Add(sheathColorPriceByType);
        }
        return sheathColorPriceByTypePresenters;
    }
}