using Domain.Component.Product.Attachments;
using Domain.Files;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class AttachmentPresenterForCanvas
{
    public Guid Id { get; set; }
    public FileEntity Model { get; set; }

    public static AttachmentPresenterForCanvas Present(Attachment attachment)
    {
        return new AttachmentPresenterForCanvas
        {
            Id = attachment.Id,
            Model = attachment.Model
        };
    }
}