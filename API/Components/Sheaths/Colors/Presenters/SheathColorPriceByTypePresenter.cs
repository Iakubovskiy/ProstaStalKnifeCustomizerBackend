using Application.Currencies;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Sheaths.Color;

namespace API.Components.Sheaths.Colors.Presenters;

public class SheathColorPriceByTypePresenter
{
    public BladeShapeType BladeShapeType { get; set; }
    public double Price { get; set; }

    public static async Task<SheathColorPriceByTypePresenter> Present(
        SheathColorPriceByType sheathColorPriceByType, 
        string locale,
        string currency,
        IPriceService priceService)
    {
        return new SheathColorPriceByTypePresenter
        {
            BladeShapeType = sheathColorPriceByType.Type,
            Price = await priceService.GetPrice(sheathColorPriceByType.Price, currency)
        };
    }

    public static async Task<List<SheathColorPriceByTypePresenter>> PresentList(
        List<SheathColorPriceByType> sheathColorPriceByTypes, 
        string locale, 
        string currency,
        IPriceService priceService)
    {
        var sheathColorPriceByTypePresenters = new List<SheathColorPriceByTypePresenter>();
        foreach (SheathColorPriceByType colorPriceByType in sheathColorPriceByTypes)
        {
            SheathColorPriceByTypePresenter sheathColorPriceByType = await Present(colorPriceByType, locale, currency, priceService);
            sheathColorPriceByTypePresenters.Add(sheathColorPriceByType);
        }
        return sheathColorPriceByTypePresenters;
    }
}