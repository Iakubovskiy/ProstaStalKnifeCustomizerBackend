using Domain.Component.Product.Attachments;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class AttachmentPresenterForCanvas
{
    public Guid Id { get; set; }
    public string ModelUrl { get; set; }

    public AttachmentPresenterForCanvas Present(Attachment attachment)
    {
        this.Id = attachment.Id;
        this.ModelUrl = attachment.Model.FileUrl;
        
        return this;
    }
    
}