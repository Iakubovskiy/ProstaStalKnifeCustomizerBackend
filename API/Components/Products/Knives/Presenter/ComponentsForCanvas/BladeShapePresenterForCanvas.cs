using Domain.Component.BladeShapes;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class BladeShapePresenterForCanvas
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public FileEntity BladeShapeModel { get; set; }
    public FileEntity? SheathModel { get; set; }

    public static BladeShapePresenterForCanvas Present(BladeShape bladeShape, string locale)
    {
        BladeShapePresenterForCanvas bladeShapePresenterForCanvas  = new BladeShapePresenterForCanvas();
        bladeShapePresenterForCanvas.Id = bladeShape.Id;
        bladeShapePresenterForCanvas.Name = bladeShape.Name.GetTranslation(locale);
        bladeShapePresenterForCanvas.BladeShapeModel = bladeShape.BladeShapeModel;
        if(bladeShape.Sheath != null)
        {
            bladeShapePresenterForCanvas.SheathModel = bladeShape.Sheath.Model;
        }
        
        return bladeShapePresenterForCanvas;
    }
}