using Domain.Component.Product.Attachments;

namespace API.Components.Products.Knives.Presenter.ComponentsForCanvas;

public class AttachmentPresenterForCanvas
{
    public Guid Id { get; set; }
    public string modelUrl { get; set; }

    public AttachmentPresenterForCanvas Present(Attachment attachment)
    {
        this.Id = attachment.Id;
        this.modelUrl = attachment.Model.FileUrl;
        
        return this;
    }
    
}