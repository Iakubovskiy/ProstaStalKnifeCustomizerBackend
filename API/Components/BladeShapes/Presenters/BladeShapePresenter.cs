using Application.Components.Prices;
using Domain.Component.BladeShapes;

namespace API.Components.BladeShapes.Presenters;

public class BladeShapePresenter
{
    private readonly IGetComponentPrice _getComponentPriceService;

    public BladeShapePresenter(IGetComponentPrice getComponentPriceService)
    {
        this._getComponentPriceService = getComponentPriceService;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BladeShapeModelUrl { get; set; }
    public string? SheathModelUrl { get; set; }

    public BladeShapePresenter Present(BladeShape bladeShape, string locale, string currency)
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

    public List<BladeShapePresenter> PresentList(List<BladeShape> bladeShapes, string locale, string curency)
    {
        List<BladeShapePresenter> bladeShapesPresenters = new List<BladeShapePresenter>();
        foreach (BladeShape bladeShape in bladeShapes)
        {
            BladeShapePresenter bladeShapePresenter = new BladeShapePresenter(_getComponentPriceService);
            bladeShapePresenter.Present(bladeShape, locale, curency);
            bladeShapesPresenters.Add(bladeShapePresenter);
        }
        
        return bladeShapesPresenters;
    }
}