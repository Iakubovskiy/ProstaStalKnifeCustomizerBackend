using API.Components.Engravings.Presenters;
using API.Components.Products.AllProducts.Presenters;
using API.Components.Products.Attachments.Presenters;
using API.Components.Sheaths.Colors.Presenters;
using API.Components.Sheaths.Presenter;
using Application.Components.Prices;
using Application.Currencies;
using Domain.Component.Product.CompletedSheath;
using Domain.Files;

namespace API.Components.Products.CompletedSheaths.Presenters;

public class CompletedSheathPresenter : AbstractProductPresenter
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public FileEntity Image { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public string Title { get; set; }
    public Dictionary<string, string> Titles { get; set; }
    public string Description { get; set; }
    public Dictionary<string, string> Descriptions { get; set; }
    public string MetaTitle { get; set; }
    public Dictionary<string, string> MetaTitles { get; set; }
    public string MetaDescription { get; set; }
    public Dictionary<string, string> MetaDescriptions { get; set; }
    public SheathPresenter Sheath { get; set; }
    public SheathColorPresenter SheathColor { get; set; }
    public List<EngravingPresenter>? Engravings { get; set; }
    public List<AttachmentPresenter>? Attachments { get; set; }
    public double TotalPrice { get; set; }
    public List<ReviewPresenter>? Reviews {get; set;}
    public double? AverageRating { get; set; } = null;

    public static async Task<CompletedSheathPresenter> Present(
        CompletedSheath sheath, 
        string locale, 
        string currency,
        IGetComponentPrice getComponentPrice,
        IPriceService priceService)
    {
        var presenter = new CompletedSheathPresenter
        {
            Id = sheath.Id,
            IsActive = sheath.IsActive,
            Name = sheath.Name.GetTranslation(locale),
            Image = sheath.Image,
            Title = sheath.Title.GetTranslation(locale),
            Description = sheath.Description.GetTranslation(locale),
            MetaTitle = sheath.MetaTitle.GetTranslation(locale),
            MetaDescription = sheath.MetaDescription.GetTranslation(locale),
            TotalPrice = await getComponentPrice.GetPrice(sheath, currency),
            Sheath = await SheathPresenter.Present(sheath.Sheath, locale, currency, getComponentPrice),
            SheathColor = await SheathColorPresenter.Present(sheath.SheathColor, locale, currency, priceService)
        };
        
        if(sheath.Engravings != null)
        {
            presenter.Engravings = await EngravingPresenter.PresentList(sheath.Engravings, locale);
        }

        if (sheath.Attachments != null)
        {
            presenter.Attachments = await AttachmentPresenter.PresentList(sheath.Attachments, locale, currency, getComponentPrice);
        }
        
        if (sheath.Reviews != null && sheath.Reviews.Any())
        {
            presenter.Reviews = ReviewPresenter.PresentList(sheath.Reviews);
            presenter.AverageRating = Math.Round(presenter.Reviews.Average(r => r.Rating), 2);
        }
        
        return presenter;
    }
    
    public static async Task<CompletedSheathPresenter> PresentWithTranslations(
        CompletedSheath sheath, 
        string locale, 
        string currency,
        IGetComponentPrice getComponentPrice,
        IPriceService priceService)
    {
        CompletedSheathPresenter presenter = await Present(sheath, locale, currency, getComponentPrice, priceService);
        presenter.Names = sheath.Name.TranslationDictionary;
        presenter.Titles = sheath.Title.TranslationDictionary;
        presenter.Descriptions = sheath.Description.TranslationDictionary;
        presenter.MetaTitles = sheath.MetaTitle.TranslationDictionary;
        presenter.MetaDescriptions = sheath.MetaDescription.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<CompletedSheathPresenter>> PresentList(
        List<CompletedSheath> sheaths, 
        string locale, 
        string currency,
        IGetComponentPrice getComponentPrice,
        IPriceService priceService)
    {
        var result = new List<CompletedSheathPresenter>();
        foreach (var sheath in sheaths)
        {
            CompletedSheathPresenter sheathPresenter = await Present(sheath, locale, currency, getComponentPrice, priceService);
            result.Add(sheathPresenter);
        }

        return result;
    }
}