using Domain.Component.BladeCoatingColors;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class BladeCoatingColorPresenterForCanvas
{
    public Guid Id { get; set; }
    public string ColorCode { get; set; }
    public string EngravingColorCode { get; set; }
    public FileEntity? ColorMap { get; set; }
    public FileEntity? NormalMap { get; set; }
    public FileEntity? RoughnessMap { get; set; }

    public static BladeCoatingColorPresenterForCanvas Present(BladeCoatingColor bladeCoatingColor)
    {
        BladeCoatingColorPresenterForCanvas bladeCoatingColorPresenterForCanvas  = new BladeCoatingColorPresenterForCanvas();
        bladeCoatingColorPresenterForCanvas.Id = bladeCoatingColor.Id;
        bladeCoatingColorPresenterForCanvas.ColorCode = bladeCoatingColor.ColorCode;
        bladeCoatingColorPresenterForCanvas.EngravingColorCode = bladeCoatingColor.EngravingColorCode;
        bladeCoatingColorPresenterForCanvas.ColorMap = bladeCoatingColor.ColorMap;
        bladeCoatingColorPresenterForCanvas.NormalMap = bladeCoatingColor.Texture?.NormalMap;
        bladeCoatingColorPresenterForCanvas.RoughnessMap = bladeCoatingColor.Texture?.RoughnessMap;
        
        return bladeCoatingColorPresenterForCanvas;
    }
}