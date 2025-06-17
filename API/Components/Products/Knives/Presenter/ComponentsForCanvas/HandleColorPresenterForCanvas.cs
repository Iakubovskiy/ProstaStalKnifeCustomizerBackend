using Domain.Component.Handles;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class HandleColorPresenterForCanvas
{
    public Guid Id { get; set; }
    public string ColorCode { get; set; }
    public string? ModelUrl { get; set; }
    public string? ColorMapUrl { get; set; }
    public string? NormalMapUrl { get; set; }
    public string? RoughnessMapUrl { get; set; }

    public HandleColorPresenterForCanvas Present(Handle handle)
    {
        this.Id = handle.Id;
        this.ColorCode = handle.ColorCode;
        this.ModelUrl = handle.HandleModel?.FileUrl;
        this.ColorMapUrl = handle.ColorMap?.FileUrl;
        this.NormalMapUrl = handle.Texture?.NormalMap.FileUrl;
        this.RoughnessMapUrl = handle.Texture?.RoughnessMap.FileUrl;
        
        return this;
    }
}