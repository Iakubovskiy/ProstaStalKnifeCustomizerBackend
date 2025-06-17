using Domain.Component.Engravings;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class EngravingPresenterForCanvas
{
    public Guid Id { get; private set; }
    public int Side { get; private set; }
    public string? Text { get; private set; }
    public string? Font { get; private set; }
    public string? PictureUrl { get; private set; }
    public double LocationX { get; private set; }
    public double LocationY { get; private set; }
    public double LocationZ { get; private set; }
    public double RotationX { get; private set; }
    public double RotationY { get; private set; }
    public double RotationZ { get; private set; }
    public double ScaleX { get; private set; }
    public double ScaleY { get; private set; }
    public double ScaleZ { get; private set; }

    public EngravingPresenterForCanvas Present(Engraving engraving)
    {
        this.Id = engraving.Id;
        this.Side = engraving.Side;
        this.Text = engraving.Text;
        this.Font = engraving.Font;
        if (engraving.Picture != null)
        {
            this.PictureUrl = engraving.Picture.FileUrl;
        }

        this.LocationX = engraving.EngravingPosition.LocationX;
        this.LocationY = engraving.EngravingPosition.LocationY;
        this.LocationZ = engraving.EngravingPosition.LocationZ;
        
        this.RotationX = engraving.EngravingRotation.RotationX;
        this.RotationY = engraving.EngravingRotation.RotationY;
        this.RotationZ = engraving.EngravingRotation.RotationZ;
        
        this.ScaleX = engraving.EngravingScale.ScaleX;
        this.ScaleY = engraving.EngravingScale.ScaleY;
        this.ScaleZ = engraving.EngravingScale.ScaleZ;
        
        return this;
    }
}