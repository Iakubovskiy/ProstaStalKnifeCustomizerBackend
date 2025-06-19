using Domain.Component.Product.Attachments;

namespace API.Components.Products.Attachments.Types.Presenter;

public class AttachmentTypePresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public async Task<AttachmentTypePresenter> Present(AttachmentType attachmentType, string locale)
    {
        this.Id = attachmentType.Id;
        this.Name = attachmentType.Name.GetTranslation(locale);
        
        return this;
    }

    public async Task<List<AttachmentTypePresenter>> PresentList(List<AttachmentType> attachmentTypes, string locale)
    {
        List<AttachmentTypePresenter> attachmentTypePresenters = new List<AttachmentTypePresenter>();
        foreach (AttachmentType type in attachmentTypes)
        {
            AttachmentTypePresenter attachmentTypePresenter = new AttachmentTypePresenter();
            await attachmentTypePresenter.Present(type, locale);
            attachmentTypePresenters.Add(attachmentTypePresenter);
        }
        return attachmentTypePresenters;
    }
}