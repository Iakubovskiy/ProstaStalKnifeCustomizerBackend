using API.Components.Products.AllProducts.Presenters;
using Application.Components.Prices;
using Domain.Component.Product.Knife;
using Domain.Files;
using Domain.Translation;

namespace API.Components.Products.Knives.Presenter;

public class KnifePresenter : AbstractProductPresenter
{
    private readonly IGetComponentPrice _getComponentPriceService;

    public KnifePresenter(IGetComponentPrice getComponentPriceService)
    {
        this._getComponentPriceService = getComponentPriceService;
    }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Dictionary<string, string> Titles { get; set; }
    public string MetaTitle { get; set; }
    public Dictionary<string, string> MetaTitles { get; set; }
    public string MetaDescription { get; set; }
    public Dictionary<string, string> MetaDescriptions { get; set; }
    public string Name {get; set;}
    public Dictionary<string, string> Names {get; set;}
    public string Description {get; set;}
    public Dictionary<string, string> Descriptions {get; set;}
    public double Price {get; set;}
    public FileEntity ImageUrl {get; set;}
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
    public List<ReviewPresenter>? Reviews {get; set;}
    public double? AverageRating { get; set; } = null;
    
    public KnifeForCanvasPresenter KnifeForCanvas  {get; set;}

    public async Task<KnifePresenter> Present(Knife knife, string locale, string currency)
    {
        this.Id = knife.Id;
        this.Title = knife.Title.GetTranslation(locale);
        this.Name = knife.Name.GetTranslation(locale);
        this.Description = knife.Description.GetTranslation(locale);
        this.Price = await this._getComponentPriceService.GetPrice(knife, currency);
        this.ImageUrl = knife.Image;
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

        if (knife.Reviews != null)
        {
            ReviewPresenter reviewPresenter = new ReviewPresenter();
            this.Reviews = reviewPresenter.PresentList(knife.Reviews);
            this.AverageRating = Math.Round(((double)this.Reviews.Sum(r => r.Rating) / this.Reviews.Count), 2);
        }
        
        return this;
    }

    public async Task<KnifePresenter> PresentWithTranslations(Knife knife, string locale, string currency)
    {
        await this.Present(knife, locale, currency);
        this.Names = knife.Name.TranslationDictionary;
        this.Titles = knife.Title.TranslationDictionary;
        this.Descriptions = knife.Description.TranslationDictionary;
        this.MetaTitles = knife.MetaTitle.TranslationDictionary;
        this.MetaDescriptions = knife.MetaDescription.TranslationDictionary;
        return this;
    }
    
    public async Task<List<KnifePresenter>> PresentList(List<Knife> knives, string locale, string currency)
    {
        List<KnifePresenter> knifePresenters = new List<KnifePresenter>();
        foreach (var knife in knives)
        {
            KnifePresenter presenter = new KnifePresenter(this._getComponentPriceService);
            await presenter.Present(knife, locale, currency);
            knifePresenters.Add(presenter);
        }

        return knifePresenters;
    }

}