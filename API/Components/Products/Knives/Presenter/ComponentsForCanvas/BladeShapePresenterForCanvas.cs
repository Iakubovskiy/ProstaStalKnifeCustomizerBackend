using Domain.Component.BladeShapes;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class BladeShapePresenterForCanvas
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BladeShapeModelUrl { get; set; }
    public string? SheathModelUrl { get; set; }

    public BladeShapePresenterForCanvas Present(BladeShape bladeShape, string locale)
    {
        this.Id = bladeShape.Id;
        this.Name = bladeShape.Name.GetTranslation(locale);
        this.BladeShapeModelUrl = bladeShape.BladeShapeModel.FileUrl;
        if(bladeShape.Sheath != null)
        {
            this.SheathModelUrl = bladeShape.Sheath.Model?.FileUrl;
        }
        
        return this;
    }
}