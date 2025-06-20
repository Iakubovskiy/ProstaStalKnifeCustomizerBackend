using Application.Currencies;
using Domain.Component.Handles;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class HandleColorPresenterForCanvas
{
    public Guid Id { get; set; }
    public string Color { get; set; }
    public string ColorCode { get; set; }
    public double Price { get; set; }
    public string? ModelUrl { get; set; }
    public FileEntity? ColorMap { get; set; }
    public FileEntity? NormalMap { get; set; }
    public FileEntity? RoughnessMap { get; set; }

    public static async Task<HandleColorPresenterForCanvas> Present(
        Handle handle, 
        string locale, 
        string currency, 
        IPriceService priceService
    )
    {
        HandleColorPresenterForCanvas handleColorPresenterForCanvas  = new HandleColorPresenterForCanvas();
        handleColorPresenterForCanvas.Id = handle.Id;
        handleColorPresenterForCanvas.Color = handle.Color.GetTranslation(locale);
        handleColorPresenterForCanvas.ColorCode = handle.ColorCode;
        handleColorPresenterForCanvas.ModelUrl = handle.HandleModel?.FileUrl;
        handleColorPresenterForCanvas.ColorMap = handle.ColorMap;
        handleColorPresenterForCanvas.NormalMap = handle.Texture?.NormalMap;
        handleColorPresenterForCanvas.RoughnessMap = handle.Texture?.RoughnessMap;
        handleColorPresenterForCanvas.Price = await priceService.GetPrice(handle.Price, currency);
        
        return handleColorPresenterForCanvas;
    }
}