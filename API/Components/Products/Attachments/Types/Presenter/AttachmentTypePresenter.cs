using Domain.Component.Product.Attachments;

namespace API.Components.Products.Attachments.Types.Presenter;

public class AttachmentTypePresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }

    public static async Task<AttachmentTypePresenter> Present(AttachmentType attachmentType, string locale)
    {
        if (attachmentType == null)
            throw new ArgumentNullException(nameof(attachmentType));
        
        var presenter = new AttachmentTypePresenter
        {
            Id = attachmentType.Id,
            Name = attachmentType.Name.GetTranslation(locale)
        };
        
        return presenter;
    }

    public static async Task<AttachmentTypePresenter> PresentWithTranslations(AttachmentType attachmentType, string locale)
    {
        AttachmentTypePresenter presenter = await Present(attachmentType, locale);
        presenter.Names = attachmentType.Name.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<AttachmentTypePresenter>> PresentList(List<AttachmentType> attachmentTypes, string locale)
    {
        List<AttachmentTypePresenter> attachmentTypePresenters = new List<AttachmentTypePresenter>();
        foreach (AttachmentType type in attachmentTypes)
        {
            AttachmentTypePresenter attachmentTypePresenter = await Present(type, locale);
            attachmentTypePresenters.Add(attachmentTypePresenter);
        }
        return attachmentTypePresenters;
    }
}