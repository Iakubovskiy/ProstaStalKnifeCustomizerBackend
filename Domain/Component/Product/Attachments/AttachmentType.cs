using Domain.Translation;

namespace Domain.Component.Product.Attachments;

public class AttachmentType : IEntity, IUpdatable<AttachmentType>
{
    private AttachmentType()
    {
        
    }
    public AttachmentType(
        Guid id,
        Translations name
    )
    {
        this.Id = id;
        this.Name = name;
    }
    public Guid Id { get; private set; }
    public Translations Name { get; private set; }
    public void Update(AttachmentType attachmentType)
    {
        this.Name = attachmentType.Name;
    }
}