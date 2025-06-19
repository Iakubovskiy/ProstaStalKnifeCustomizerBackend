using API.Components.Products.AllProducts.Presenters;
using API.Components.Products.Attachments.Types.Presenter;
using Application.Components.Prices;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.Reviews;
using Domain.Files;

namespace API.Components.Products.Attachments.Presenters;

public class AttachmentPresenter : AbstractProductPresenter
{
    private readonly IGetComponentPrice _getComponentPriceService;

    public AttachmentPresenter(IGetComponentPrice getComponentPriceService)
    {
        this._getComponentPriceService = getComponentPriceService;
    }
    
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public FileEntity Image { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string MetaTitle { get; set; }
    public string MetaDescription { get; set; }
    public AttachmentTypePresenter Type { get; set; }
    public string Material { get; set; }
    public double Price { get; set; }
    public FileEntity Model { get; set; }
    public List<ReviewPresenter>? Reviews {get; set;}
    public double? AverageRating { get; set; } = null;

    public async Task<AttachmentPresenter> Present(Attachment attachment, string locale, string currency)
    {
        this.Id = attachment.Id;
        this.IsActive = attachment.IsActive;
        this.Image = attachment.Image;
        this.Name = attachment.Name.GetTranslation(locale);
        this.Title = attachment.Title.GetTranslation(locale);
        this.Description = attachment.Description.GetTranslation(locale);
        this.MetaTitle = attachment.MetaTitle.GetTranslation(locale);
        this.MetaDescription = attachment.MetaDescription.GetTranslation(locale);
        AttachmentTypePresenter attachmentTypePresenter = new AttachmentTypePresenter();
        this.Type = await attachmentTypePresenter.Present(attachment.Type, locale);
        this.Material = attachment.Material.GetTranslation(locale);
        this.Price = await this._getComponentPriceService.GetPrice(attachment, currency);
        this.Model = attachment.Model;
        
        if (attachment.Reviews != null)
        {
            ReviewPresenter reviewPresenter = new ReviewPresenter();
            this.Reviews = reviewPresenter.PresentList(attachment.Reviews);
            this.AverageRating = Math.Round(((double)this.Reviews.Sum(r => r.Rating) / this.Reviews.Count), 2);
        }
        
        return this;
    }

    public async Task<List<AttachmentPresenter>> PresentList(List<Attachment> attachments, string locale,
        string currency)
    {
        var tasks = attachments.Select(
            async attachment =>
            {
                var presenter = new AttachmentPresenter(this._getComponentPriceService);
                return await presenter.Present(attachment, locale, currency);
            }
        );
        return (await Task.WhenAll(tasks)).ToList();
    }
}