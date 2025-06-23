using Application.Currencies;
using Domain.Component.Product.Attachments;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class AttachmentPresenterForCanvas
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public FileEntity Iamge { get; set; }
    public FileEntity Model { get; set; }
    public string Color { get; set; }
    public string Material { get; set; }

    public static async Task<AttachmentPresenterForCanvas> Present(
        Attachment attachment,
        string locale,
        string currency,
        IPriceService priceService
    )
    {
        return new AttachmentPresenterForCanvas
        {
            Id = attachment.Id,
            Model = attachment.Model,
            Name = attachment.Name.GetTranslation(locale),
            Price = await priceService.GetPrice(attachment.Price, currency),
            Iamge = attachment.Image,
            Color = attachment.Color.GetTranslation(locale),
            Material = attachment.Material.GetTranslation(locale),
        };
    }
}