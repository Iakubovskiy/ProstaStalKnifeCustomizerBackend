using API.Components.Products.AllProducts.Presenters;
using API.Components.Products.Attachments.Types.Presenter;
using Application.Components.Prices;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.Reviews;
using Domain.Files;

namespace API.Components.Products.Attachments.Presenters;

public class AttachmentPresenter : AbstractProductPresenter
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
    public AttachmentTypePresenter Type { get; set; }
    public string Material { get; set; }
    public Dictionary<string, string> Materials { get; set; }
    public double Price { get; set; }
    public FileEntity Model { get; set; }
    public List<ReviewPresenter>? Reviews {get; set;}
    public double? AverageRating { get; set; } = null;

    public static async Task<AttachmentPresenter> Present(
        Attachment attachment, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        var presenter = new AttachmentPresenter
        {
            Id = attachment.Id,
            IsActive = attachment.IsActive,
            Image = attachment.Image,
            Name = attachment.Name.GetTranslation(locale),
            Title = attachment.Title.GetTranslation(locale),
            Description = attachment.Description.GetTranslation(locale),
            MetaTitle = attachment.MetaTitle.GetTranslation(locale),
            MetaDescription = attachment.MetaDescription.GetTranslation(locale),
            Type = await AttachmentTypePresenter.Present(attachment.Type, locale),
            Material = attachment.Material.GetTranslation(locale),
            Price = await getComponentPriceService.GetPrice(attachment, currency),
            Model = attachment.Model
        };
        
        if (attachment.Reviews != null && attachment.Reviews.Any())
        {
            presenter.Reviews = ReviewPresenter.PresentList(attachment.Reviews);
            presenter.AverageRating = Math.Round(presenter.Reviews.Average(r => r.Rating), 2);
        }
        
        return presenter;
    }
    
    public static async Task<AttachmentPresenter> PresentWithTranslations(
        Attachment attachment, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        AttachmentPresenter presenter = await Present(attachment, locale, currency, getComponentPriceService);
        presenter.Names = attachment.Name.TranslationDictionary;
        presenter.Titles = attachment.Title.TranslationDictionary;
        presenter.Descriptions = attachment.Description.TranslationDictionary;
        presenter.MetaTitles = attachment.MetaTitle.TranslationDictionary;
        presenter.MetaDescriptions = attachment.MetaDescription.TranslationDictionary;
        presenter.Materials = attachment.Material.TranslationDictionary;
        
        return presenter;
    }

    public static async Task<List<AttachmentPresenter>> PresentList(
        List<Attachment> attachments, 
        string locale,
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        var attachmentPresenters = new List<AttachmentPresenter>();
        foreach (var attachment in attachments)
        {
            AttachmentPresenter attachmentPresenter = await Present(attachment, locale, currency, getComponentPriceService);
            attachmentPresenters.Add(attachmentPresenter);
        }
        return attachmentPresenters;
    }
}