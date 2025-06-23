using Application.Currencies;
using Domain.Component.BladeShapes;
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class BladeShapePresenterForCanvas
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public FileEntity? BladeShapeImage { get; set; }
    public FileEntity BladeShapeModel { get; set; }
    public FileEntity? SheathModel { get; set; }
    public BladeShapeType ShapeType { get; set; }
    public Guid? SheathId {get; set;}
    public BladeCharacteristics BladeCharacteristicsModel { get; set; }

    public static async Task<BladeShapePresenterForCanvas> Present(
        BladeShape bladeShape, 
        string locale,
        string currency,
        IPriceService priceService
    )
    {
        BladeShapePresenterForCanvas bladeShapePresenterForCanvas  = new BladeShapePresenterForCanvas();
        bladeShapePresenterForCanvas.Id = bladeShape.Id;
        bladeShapePresenterForCanvas.Name = bladeShape.Name.GetTranslation(locale);
        bladeShapePresenterForCanvas.BladeShapeModel = bladeShape.BladeShapeModel;
        bladeShapePresenterForCanvas.BladeCharacteristicsModel = bladeShape.BladeCharacteristics;
        if(bladeShape.Sheath != null)
        {
            bladeShapePresenterForCanvas.SheathModel = bladeShape.Sheath.Model;
        }
        bladeShapePresenterForCanvas.Price = await priceService.GetPrice(bladeShape.Price, currency);
        bladeShapePresenterForCanvas.ShapeType = bladeShape.Type;
        bladeShapePresenterForCanvas.SheathId = bladeShape.Sheath?.Id;
        bladeShapePresenterForCanvas.BladeShapeImage = bladeShape.BladeShapePhoto;
        
        return bladeShapePresenterForCanvas;
    }
}