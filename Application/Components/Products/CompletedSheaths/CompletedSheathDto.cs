using Application.Components.Products.Attachemts;
using Application.Components.SimpleComponents.Engravings;

namespace Application.Components.Products.CompletedSheaths;

public class CompletedSheathDto: IProductDto
{
    public CompletedSheathDto(
        Guid? id, 
        bool isActive, 
        Guid imageFileId, 
        Dictionary<string, string> name, 
        Dictionary<string, string> title, 
        Dictionary<string, string> description, 
        Dictionary<string, string> metaTitle, 
        Dictionary<string, string> metaDescription, 
        List<Guid> tagsIds, 
        Guid sheathId, 
        Guid sheathColorId, 
        List<Guid> existingEngravingIds, 
        List<EngravingDto> newEngravings
    )
    {
        Id = id;
        IsActive = isActive;
        ImageFileId = imageFileId;
        Name = name;
        Title = title;
        Description = description;
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        TagsIds = tagsIds;
        SheathId = sheathId;
        SheathColorId = sheathColorId;
        ExistingEngravingIds = existingEngravingIds;
        NewEngravings = newEngravings;
    }

    public Guid? Id { get; private set; }
    public bool IsActive { get; private set; }
    public Guid ImageFileId { get; private set; }
    public Dictionary<string, string> Name { get; private set; }
    public Dictionary<string, string> Title { get; private set; }
    public Dictionary<string, string> Description { get; private set; }
    public Dictionary<string, string> MetaTitle { get; private set; }
    public Dictionary<string, string> MetaDescription { get; private set; }
    public List<Guid> TagsIds { get; private set; }
    
    public Guid SheathId { get; private set; }
    public Guid SheathColorId { get; private set; }
    public List<Guid> ExistingEngravingIds { get; private set; }
    public List<EngravingDto> NewEngravings { get; private set; }
    public List<Guid> ExistingAttachmentIds { get; private set; }
    public List<AttachmentDto> NewAttachments { get; private set; }
}