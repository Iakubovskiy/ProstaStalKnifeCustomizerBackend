using Application.Currencies;
using Domain.Component.BladeCoatingColors;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class BladeCoatingColorPresenterForCanvas
{
    public Guid Id { get; set; }
    public double Price { get; set; }
    public string Color { get; set; }
    public string Type { get; set; }
    public string ColorCode { get; set; }
    public string EngravingColorCode { get; set; }
    public FileEntity? ColorMap { get; set; }
    public FileEntity? NormalMap { get; set; }
    public FileEntity? RoughnessMap { get; set; }

    public static async Task<BladeCoatingColorPresenterForCanvas> Present(
        BladeCoatingColor bladeCoatingColor,
        string locale,
        string currency,
        IPriceService priceService
    )
    {
        BladeCoatingColorPresenterForCanvas bladeCoatingColorPresenterForCanvas  = new BladeCoatingColorPresenterForCanvas();
        bladeCoatingColorPresenterForCanvas.Id = bladeCoatingColor.Id;
        bladeCoatingColorPresenterForCanvas.Color = bladeCoatingColor.Color.GetTranslation(locale);
        bladeCoatingColorPresenterForCanvas.ColorCode = bladeCoatingColor.ColorCode;
        bladeCoatingColorPresenterForCanvas.EngravingColorCode = bladeCoatingColor.EngravingColorCode;
        bladeCoatingColorPresenterForCanvas.ColorMap = bladeCoatingColor.ColorMap;
        bladeCoatingColorPresenterForCanvas.NormalMap = bladeCoatingColor.Texture?.NormalMap;
        bladeCoatingColorPresenterForCanvas.RoughnessMap = bladeCoatingColor.Texture?.RoughnessMap;
        bladeCoatingColorPresenterForCanvas.Price = await priceService.GetPrice(bladeCoatingColor.Price, currency);
        bladeCoatingColorPresenterForCanvas.Type = bladeCoatingColor.Type.GetTranslation(locale);
        
        return bladeCoatingColorPresenterForCanvas;
    }
}