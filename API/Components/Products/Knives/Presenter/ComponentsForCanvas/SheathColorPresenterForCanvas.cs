using API.Components.Sheaths.Colors.Presenters;
using Application.Currencies;
using Domain.Component.Sheaths.Color;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class SheathColorPresenterForCanvas
{
    public Guid Id { get; set; }
    public List<SheathColorPriceByTypePresenter> Prices { get; set; }
    public FileEntity? ColorMap { get; set; }
    public FileEntity? NormalMap { get; set; }
    public FileEntity? RoughnessMap { get; set; }
    public string Color { get; set; }
    public string ColorCode { get; set; }
    public string EngravingColorCode { get; set; }
    public double BasePrice { get; set; }

    public static async Task<SheathColorPresenterForCanvas> Present(
        SheathColor sheathColor, 
        string locale, 
        string currency,
        IPriceService priceService,
        double sheathPrice
    )
    {
        SheathColorPresenterForCanvas sheathColorPresenter = new SheathColorPresenterForCanvas();
        sheathColorPresenter.Id = sheathColor.Id;
        sheathColorPresenter.ColorMap = sheathColor.ColorMap;
        sheathColorPresenter.NormalMap = sheathColor.Texture?.NormalMap;
        sheathColorPresenter.RoughnessMap = sheathColor.Texture?.RoughnessMap;
        sheathColorPresenter.ColorCode = sheathColor.ColorCode;
        sheathColorPresenter.EngravingColorCode = sheathColor.EngravingColorCode;
        sheathColorPresenter.Prices = await SheathColorPriceByTypePresenter
            .PresentList(sheathColor.Prices, locale, currency, priceService);
        sheathColorPresenter.Color = sheathColor.Color.GetTranslation(locale);
        sheathColorPresenter.BasePrice = sheathPrice;
        
        return sheathColorPresenter;
    }
}