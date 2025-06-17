using Domain.Component.BladeCoatingColors;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class BladeCoatingColorPresenterForCanvas
{
    public Guid Id { get; set; }
    public string ColorCode { get; set; }
    public string EngravingColorCode { get; set; }
    public string? ColorMapUrl { get; set; }
    public string? NormalMapUrl { get; set; }
    public string? RoughnessMapUrl { get; set; }

    public BladeCoatingColorPresenterForCanvas Present(BladeCoatingColor bladeCoatingColor)
    {
        this.Id = bladeCoatingColor.Id;
        this.ColorCode = bladeCoatingColor.ColorCode;
        this.EngravingColorCode = bladeCoatingColor.EngravingColorCode;
        this.ColorMapUrl = bladeCoatingColor.ColorMap?.FileUrl;
        this.NormalMapUrl = bladeCoatingColor.Texture?.NormalMap.FileUrl;
        this.RoughnessMapUrl = bladeCoatingColor.Texture?.RoughnessMap.FileUrl;
        
        return this;
    }
}