using API.Components.Engravings.Presenters;
using API.Components.Products.AllProducts.Presenters;
using API.Components.Products.Attachments.Presenters;
using API.Components.Sheaths.Colors.Presenters;
using API.Components.Sheaths.Presenter;
using Application.Components.Prices;
using Domain.Component.Product.CompletedSheath;
using Domain.Files;

namespace API.Components.Products.CompletedSheaths.Presenters;

public class CompletedSheathPresenter : AbstractProductPresenter
{
    private readonly IGetComponentPrice _getComponentPrice;
    private readonly SheathPresenter _sheathPresenter;
    private readonly SheathColorPresenter _sheathColorPresenter;
    private readonly AttachmentPresenter _attachmentPresenter;

    public CompletedSheathPresenter(
        IGetComponentPrice getComponentPrice,
        SheathPresenter sheathPresenter,
        SheathColorPresenter sheathColorPresenter,
        AttachmentPresenter attachmentPresenter
    )
    {
        this._getComponentPrice = getComponentPrice;
        this._sheathPresenter = sheathPresenter;
        this._sheathColorPresenter = sheathColorPresenter;
        this._attachmentPresenter = attachmentPresenter;
    }
    
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

    public async Task<CompletedSheathPresenter> Present(CompletedSheath sheath, string locale, string currency)
    {
        this.Id = sheath.Id;
        this.IsActive = sheath.IsActive;
        this.Name = sheath.Name.GetTranslation(locale);
        this.Image = sheath.Image;
        this.Title = sheath.Title.GetTranslation(locale);
        this.Description = sheath.Description.GetTranslation(locale);
        this.MetaTitle = sheath.MetaTitle.GetTranslation(locale);
        this.MetaDescription = sheath.MetaDescription.GetTranslation(locale);
        this.TotalPrice = await this._getComponentPrice.GetPrice(sheath, currency);
        this.Sheath = await this._sheathPresenter.Present(sheath.Sheath, locale, currency);
        this.SheathColor = await this._sheathColorPresenter.Present(sheath.SheathColor, locale, currency);
        
        if(sheath.Engravings != null)
        {
            EngravingPresenter engravingPresenter = new EngravingPresenter();
            this.Engravings = await engravingPresenter.PresentList(sheath.Engravings, locale);
        }

        if (sheath.Attachments != null)
        {
            this.Attachments = await this._attachmentPresenter.PresentList(sheath.Attachments, locale, currency);
        }
        
        if (sheath.Reviews != null)
        {
            ReviewPresenter reviewPresenter = new ReviewPresenter();
            this.Reviews = reviewPresenter.PresentList(sheath.Reviews);
            this.AverageRating = Math.Round(((double)this.Reviews.Sum(r => r.Rating) / this.Reviews.Count), 2);
        }
        
        return this;
    }
    
    public async Task<CompletedSheathPresenter> PresentWithTranslations(CompletedSheath sheath, string locale, string currency)
    {
        await this.Present(sheath, locale, currency);
        this.Names = sheath.Name.TranslationDictionary;
        this.Titles = sheath.Title.TranslationDictionary;
        this.Descriptions = sheath.Description.TranslationDictionary;
        this.MetaTitles = sheath.MetaTitle.TranslationDictionary;
        this.MetaDescriptions = sheath.MetaDescription.TranslationDictionary;
        return this;
    }

    public async Task<List<CompletedSheathPresenter>> PresentList(List<CompletedSheath> sheaths, string locale, string currency)
    {
        var result = sheaths.Select(async sheath =>
        {
            CompletedSheathPresenter presenter = new CompletedSheathPresenter(
                this._getComponentPrice, 
                this._sheathPresenter, 
                this._sheathColorPresenter,
                this._attachmentPresenter
            );
            return await presenter.Present(sheath, locale, currency);
            
        });

        return (await Task.WhenAll(result)).ToList();
    }
}