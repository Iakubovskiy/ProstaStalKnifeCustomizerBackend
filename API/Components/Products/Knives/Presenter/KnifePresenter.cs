using API.Components.Products.AllProducts.Presenters;
using Application.Components.Prices;
using Domain.Component.Product.Knife;
using Domain.Files;
using Domain.Translation;

namespace API.Components.Products.Knives.Presenter;

public class KnifePresenter : AbstractProductPresenter
{
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
    public bool IsActive {get; set;}
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

    public static async Task<KnifePresenter> Present(
        Knife knife, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        var presenter = new KnifePresenter
        {
            Id = knife.Id,
            Title = knife.Title.GetTranslation(locale),
            Name = knife.Name.GetTranslation(locale),
            Description = knife.Description.GetTranslation(locale),
            Price = await getComponentPriceService.GetPrice(knife, currency),
            ImageUrl = knife.Image,
            TotalLength = knife.Blade.BladeCharacteristics.TotalLength,
            BladeLength = knife.Blade.BladeCharacteristics.BladeLength,
            BladeWidth = knife.Blade.BladeCharacteristics.BladeWidth,
            BladeWeight = knife.Blade.BladeCharacteristics.BladeWeight,
            SharpeningAngle = knife.Blade.BladeCharacteristics.SharpeningAngle,
            RockwellHardnessUnits = knife.Blade.BladeCharacteristics.RockwellHardnessUnits,
            BladeCoatingType = knife.Color.Type.GetTranslation(locale),
            BladeCoatingColor = knife.Color.Color.GetTranslation(locale),
            MetaTitle = knife.MetaTitle.GetTranslation(locale),
            MetaDescription = knife.MetaDescription.GetTranslation(locale),
            KnifeForCanvas = await KnifeForCanvasPresenter.Present(knife, locale),
            IsActive = knife.IsActive,
        };
        
        if (knife.Handle != null)
        {
            presenter.HandleColor = knife.Handle.Color.GetTranslation(locale);
        }

        if (knife.SheathColor != null)
        {
            presenter.SheathColor = knife.SheathColor.Color.GetTranslation(locale);
        }

        if (knife.Engravings != null && knife.Engravings.Any())
        {
            presenter.EngravingNames = knife.Engravings.Select(engraving => engraving.Name.GetTranslation(locale)).ToList();
        }

        if (knife.Reviews != null && knife.Reviews.Any())
        {
            presenter.Reviews = ReviewPresenter.PresentList(knife.Reviews);
            presenter.AverageRating = Math.Round(presenter.Reviews.Average(r => r.Rating), 2);
        }
        
        return presenter;
    }

    public static async Task<KnifePresenter> PresentWithTranslations(
        Knife knife, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        KnifePresenter presenter = await Present(knife, locale, currency, getComponentPriceService);
        presenter.Names = knife.Name.TranslationDictionary;
        presenter.Titles = knife.Title.TranslationDictionary;
        presenter.Descriptions = knife.Description.TranslationDictionary;
        presenter.MetaTitles = knife.MetaTitle.TranslationDictionary;
        presenter.MetaDescriptions = knife.MetaDescription.TranslationDictionary;
        return presenter;
    }
    
    public static async Task<List<KnifePresenter>> PresentList(
        List<Knife> knives, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        var knifePresenters = new List<KnifePresenter>();
        foreach (var knife in knives)
        {
            KnifePresenter presenter = await Present(knife, locale, currency, getComponentPriceService);
            knifePresenters.Add(presenter);
        }

        return knifePresenters;
    }
}