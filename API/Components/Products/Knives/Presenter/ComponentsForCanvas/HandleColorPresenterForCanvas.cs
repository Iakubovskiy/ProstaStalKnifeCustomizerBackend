using Domain.Component.Handles;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class HandleColorPresenterForCanvas
{
    public Guid Id { get; set; }
    public string ColorCode { get; set; }
    public string? ModelUrl { get; set; }
    public FileEntity? ColorMap { get; set; }
    public FileEntity? NormalMap { get; set; }
    public FileEntity? RoughnessMap { get; set; }

    public static HandleColorPresenterForCanvas Present(Handle handle)
    {
        HandleColorPresenterForCanvas handleColorPresenterForCanvas  = new HandleColorPresenterForCanvas();
        handleColorPresenterForCanvas.Id = handle.Id;
        handleColorPresenterForCanvas.ColorCode = handle.ColorCode;
        handleColorPresenterForCanvas.ModelUrl = handle.HandleModel?.FileUrl;
        handleColorPresenterForCanvas.ColorMap = handle.ColorMap;
        handleColorPresenterForCanvas.NormalMap = handle.Texture?.NormalMap;
        handleColorPresenterForCanvas.RoughnessMap = handle.Texture?.RoughnessMap;
        
        return handleColorPresenterForCanvas;
    }
}