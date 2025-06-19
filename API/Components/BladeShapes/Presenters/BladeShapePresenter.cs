using System.Reflection.PortableExecutable;
using Application.Components.Prices;
using Domain.Component.BladeShapes;
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Sheaths;
using Domain.Files;

namespace API.Components.BladeShapes.Presenters;

public class BladeShapePresenter
{
    private readonly IGetComponentPrice _getComponentPriceService;

    public BladeShapePresenter(IGetComponentPrice getComponentPriceService)
    {
        this._getComponentPriceService = getComponentPriceService;
    }
    
    public Guid Id { get; set; }
    public BladeShapeType ShapeType { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public double Price { get; set; }
    public FileEntity BladeShapeModel { get; set; }
    public FileEntity BladeShapeImage { get; set; }
    public Sheath? SheathModelUrl { get; set; }
    public BladeCharacteristics BladeCharacteristicsModel { get; set; }

    public async Task<BladeShapePresenter> Present(BladeShape bladeShape, string locale, string currency)
    {
        this.Id = bladeShape.Id;
        this.ShapeType = bladeShape.Type;
        this.Price = await this._getComponentPriceService.GetPrice(bladeShape, locale);
        this.Name = bladeShape.Name.GetTranslation(locale);
        this.BladeShapeModel = bladeShape.BladeShapeModel;
        this.BladeShapeImage = bladeShape.BladeShapeModel;
        this.BladeCharacteristicsModel = bladeShape.BladeCharacteristics;
        if(bladeShape.Sheath != null)
        {
            this.SheathModelUrl = bladeShape.Sheath;
        }
        
        return this;
    }

    public async Task<BladeShapePresenter> PresentWithTranslations(BladeShape bladeShape, string locale,
        string currency)
    {
        await this.Present(bladeShape, locale, currency);
        this.Names = bladeShape.Name.TranslationDictionary;
        return this;
    }

    public async Task<List<BladeShapePresenter>> PresentList(List<BladeShape> bladeShapes, string locale, string currency)
    {
        List<BladeShapePresenter> bladeShapesPresenters = new List<BladeShapePresenter>();
        foreach (BladeShape bladeShape in bladeShapes)
        {
            BladeShapePresenter bladeShapePresenter = new BladeShapePresenter(_getComponentPriceService);
            await bladeShapePresenter.Present(bladeShape, locale, currency);
            bladeShapesPresenters.Add(bladeShapePresenter);
        }
        
        return bladeShapesPresenters;
    }
}