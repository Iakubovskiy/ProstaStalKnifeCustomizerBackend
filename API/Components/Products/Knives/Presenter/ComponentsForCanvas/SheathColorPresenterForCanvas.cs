using Domain.Component.Sheaths.Color;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class SheathColorPresenterForCanvas
{
    public Guid Id { get; set; }
    public FileEntity? ColorMap { get; set; }
    public FileEntity? NormalMap { get; set; }
    public FileEntity? RoughnessMap { get; set; }
    public string ColorCode { get; set; }
    public string EngravingColorCode { get; set; }

    public static SheathColorPresenterForCanvas Present(SheathColor sheathColor)
    {
        SheathColorPresenterForCanvas sheathColorPresenter = new SheathColorPresenterForCanvas();
        sheathColorPresenter.Id = sheathColor.Id;
        sheathColorPresenter.ColorMap = sheathColor.ColorMap;
        sheathColorPresenter.NormalMap = sheathColor.Texture?.NormalMap;
        sheathColorPresenter.RoughnessMap = sheathColor.Texture?.RoughnessMap;
        sheathColorPresenter.ColorCode = sheathColor.ColorCode;
        sheathColorPresenter.EngravingColorCode = sheathColor.EngravingColorCode;
        
        return sheathColorPresenter;
    }
}