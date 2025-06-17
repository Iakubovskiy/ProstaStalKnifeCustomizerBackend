using Application.Components.Prices;
using Domain.Component.Product.Knife;

namespace API.Components.Products.Knives.Presenter;

public class KnifePresenter
{
    private readonly IGetComponentPrice _getComponentPriceService;

    public KnifePresenter(IGetComponentPrice getComponentPriceService)
    {
        this._getComponentPriceService = getComponentPriceService;
    }
    public string MetaTitle { get; set; }
    public string MetaDescription { get; set; }
    public string Name {get; set;}
    public string Description {get; set;}
    public double Price {get; set;}
    public string ImageUrl {get; set;}
    public double TotalLength { get; set; }
    public double BladeLength { get; set; }
    public double BladeWidth { get; set; }
    public double BladeWeight { get; set; }
    public double SharpeningAngle { get; set; }
    public double RockwellHardnessUnits  { get; set; }
    public string? SheathColor {get; set;}
    public string? HandleColor {get; set;}
    public string BladeCoatingColor {get; set;}
    public string BladeCoatingType {get; set;}
    public List<string>? EngravingNames {get; set;}
    
    public KnifeForCanvasPresenter KnifeForCanvas  {get; set;}

    public async Task<KnifePresenter> Present(Knife knife, string locale, string currency)
    {
        this.Name = knife.Name.GetTranslation(locale);
        this.Description = knife.Description.GetTranslation(locale);
        this.Price = await this._getComponentPriceService.GetPrice(knife, currency);
        this.ImageUrl = knife.Image.FileUrl;
        this.TotalLength = knife.Blade.BladeCharacteristics.TotalLength;
        this.BladeLength = knife.Blade.BladeCharacteristics.BladeLength;
        this.BladeWidth = knife.Blade.BladeCharacteristics.BladeWidth;
        this.BladeWeight = knife.Blade.BladeCharacteristics.BladeWeight;
        this.SharpeningAngle = knife.Blade.BladeCharacteristics.SharpeningAngle;
        this.RockwellHardnessUnits = knife.Blade.BladeCharacteristics.RockwellHardnessUnits;
        this.BladeCoatingType = knife.Color.Type.GetTranslation(locale);
        this.BladeCoatingColor = knife.Color.Color.GetTranslation(locale);
        this.MetaTitle = knife.MetaTitle.GetTranslation(locale);
        this.MetaDescription = knife.MetaDescription.GetTranslation(locale);
        
        if (knife.Handle != null)
        {
            this.HandleColor = knife.Handle.Color.GetTranslation(locale);
        }

        if (knife is { SheathColor: not null })
        {
            this.SheathColor = knife.SheathColor.Color.GetTranslation(locale);
        }

        if (knife.Engravings != null && knife.Engravings.Count > 0)
        {
            this.EngravingNames = knife.Engravings.Select(engraving => engraving.Name.GetTranslation(locale)).ToList();
        }

        KnifeForCanvasPresenter knifeForCanvasPresenter = new KnifeForCanvasPresenter();
        this.KnifeForCanvas = await knifeForCanvasPresenter.Present(knife, locale);
        
        return this;
    }
}