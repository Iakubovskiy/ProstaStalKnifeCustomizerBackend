using Domain.Component.Engravings;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class EngravingPresenterForCanvas
{
    public Guid Id { get; private set; }
    public int Side { get; private set; }
    public string? Name { get; private set; }
    public string? Text { get; private set; }
    public string? Font { get; private set; }
    public FileEntity? Picture { get; private set; }
    public double LocationX { get; private set; }
    public double LocationY { get; private set; }
    public double LocationZ { get; private set; }
    public double RotationX { get; private set; }
    public double RotationY { get; private set; }
    public double RotationZ { get; private set; }
    public double ScaleX { get; private set; }
    public double ScaleY { get; private set; }
    public double ScaleZ { get; private set; }

    public static EngravingPresenterForCanvas Present(Engraving engraving, string locale)
    {
        EngravingPresenterForCanvas engravingPresenterForCanvas  = new EngravingPresenterForCanvas();
        engravingPresenterForCanvas.Id = engraving.Id;
        engravingPresenterForCanvas.Side = engraving.Side;
        engravingPresenterForCanvas.Name = engraving.Name.GetTranslation(locale);
        engravingPresenterForCanvas.Text = engraving.Text;
        engravingPresenterForCanvas.Font = engraving.Font;
        if (engraving.Picture != null)
        {
            engravingPresenterForCanvas.Picture = engraving.Picture;
        }

        engravingPresenterForCanvas.LocationX = engraving.EngravingPosition.LocationX;
        engravingPresenterForCanvas.LocationY = engraving.EngravingPosition.LocationY;
        engravingPresenterForCanvas.LocationZ = engraving.EngravingPosition.LocationZ;
        
        engravingPresenterForCanvas.RotationX = engraving.EngravingRotation.RotationX;
        engravingPresenterForCanvas.RotationY = engraving.EngravingRotation.RotationY;
        engravingPresenterForCanvas.RotationZ = engraving.EngravingRotation.RotationZ;
        
        engravingPresenterForCanvas.ScaleX = engraving.EngravingScale.ScaleX;
        engravingPresenterForCanvas.ScaleY = engraving.EngravingScale.ScaleY;
        engravingPresenterForCanvas.ScaleZ = engraving.EngravingScale.ScaleZ;
        
        return engravingPresenterForCanvas;
    }
}