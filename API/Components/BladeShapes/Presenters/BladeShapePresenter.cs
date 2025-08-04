using System.Reflection.PortableExecutable;
using API.Components.Sheaths.Presenter;
using Application.Components.Prices;
using Domain.Component.BladeShapes;
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Sheaths;
using Domain.Files;

namespace API.Components.BladeShapes.Presenters;

public class BladeShapePresenter
{
    public Guid Id { get; set; }
    public BladeShapeType ShapeType { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public double Price { get; set; }
    public FileEntity BladeShapeModel { get; set; }
    public FileEntity? BladeShapePhoto { get; set; }
    public FileEntity? SheathModelUrl { get; set; }
    public BladeCharacteristics BladeCharacteristicsModel { get; set; }
    public SheathPresenter? Sheath { get; set; } 

    public static async Task<BladeShapePresenter> Present(
        BladeShape bladeShape, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        var presenter = new BladeShapePresenter
        {
            Id = bladeShape.Id,
            ShapeType = bladeShape.Type,
            Price = await getComponentPriceService.GetPrice(bladeShape, currency),
            Name = bladeShape.Name.GetTranslation(locale),
            BladeShapeModel = bladeShape.BladeShapeModel,
            BladeShapePhoto = bladeShape.BladeShapePhoto,
            BladeCharacteristicsModel = bladeShape.BladeCharacteristics,
            IsActive = bladeShape.IsActive,
        };

        if(bladeShape.Sheath != null)
        {
            presenter.SheathModelUrl = bladeShape.Sheath.Model;
            presenter.Sheath = await SheathPresenter.Present(bladeShape.Sheath, locale, currency, getComponentPriceService);
        }
        
        return presenter;
    }

    public static async Task<BladeShapePresenter> PresentWithTranslations(
        BladeShape bladeShape, 
        string locale,
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        BladeShapePresenter presenter = await Present(bladeShape, locale, currency, getComponentPriceService);
        presenter.Names = bladeShape.Name.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<BladeShapePresenter>> PresentList(
        List<BladeShape> bladeShapes, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        var bladeShapesPresenters = new List<BladeShapePresenter>();
        foreach (BladeShape bladeShape in bladeShapes)
        {
            BladeShapePresenter bladeShapePresenter = await Present(bladeShape, locale, currency, getComponentPriceService);
            bladeShapesPresenters.Add(bladeShapePresenter);
        }
        
        return bladeShapesPresenters;
    }
}