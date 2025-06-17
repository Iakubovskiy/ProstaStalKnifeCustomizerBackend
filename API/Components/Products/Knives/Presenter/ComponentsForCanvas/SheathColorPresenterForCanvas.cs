using Domain.Component.Sheaths.Color;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class SheathColorPresenterForCanvas
{
    public Guid Id { get; set; }
    public string? ColorMapUrl { get; set; }
    public string? NormalMapUrl { get; set; }
    public string? RoughnessMapUrl { get; set; }
    public string ColorCode { get; set; }
    public string EngravingColorCode { get; set; }

    public SheathColorPresenterForCanvas Present(SheathColor sheathColor)
    {
        this.Id = sheathColor.Id;
        this.ColorMapUrl = sheathColor.ColorMap?.FileUrl;
        this.NormalMapUrl = sheathColor.Texture?.NormalMap.FileUrl;
        this.RoughnessMapUrl = sheathColor.Texture?.RoughnessMap.FileUrl;
        this.ColorCode = sheathColor.ColorCode;
        this.EngravingColorCode = sheathColor.EngravingColorCode;
        
        return this;
    }
}