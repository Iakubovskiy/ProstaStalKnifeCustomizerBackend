using Application.Components.Products.Attachemts;
using Application.Components.SimpleComponents.Engravings;

namespace Application.Components.Products.Knives;

public class KnifeDto : IProductDto
{
    public KnifeDto(
        bool isActive, 
        Guid imageFileId, 
        Dictionary<string, string> name, 
        Dictionary<string, string> title, 
        Dictionary<string, string> description, 
        Dictionary<string, string> metaTitle, 
        Dictionary<string, string> metaDescription, 
        List<Guid> tagsIds, 
        Guid shapeId, 
        Guid bladeCoatingColorId, 
        Guid handleId, 
        Guid sheathId, 
        Guid sheathColorId,
        List<Guid> existingEngravingIds,
        List<EngravingDto> newEngravings,
        List<Guid> existingAttachmentIds,
        List<AttachmentDto> newAttachments,
        Guid? id = null
    )
    {
        this.Id = id;
        this.IsActive = isActive;
        this.ImageFileId = imageFileId;
        this.Name = name;
        this.Title = title;
        this.Description = description;
        this.MetaTitle = metaTitle;
        this.MetaDescription = metaDescription;
        this.TagsIds = tagsIds;
        this.ShapeId = shapeId;
        this.BladeCoatingColorId = bladeCoatingColorId;
        this.HandleId = handleId;
        this.SheathId = sheathId;
        this.SheathColorId = sheathColorId;
        this.ExistingEngravingIds = existingEngravingIds;
        this.NewEngravings = newEngravings;
        this.ExistingAttachmentIds = existingAttachmentIds;
        this.NewAttachments = newAttachments;
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
    
    public Guid ShapeId { get; private set; }
    public Guid BladeCoatingColorId { get; private set; }
    public Guid? HandleId { get; private set; }
    public Guid? SheathId { get; private set; }
    public Guid? SheathColorId { get; private set; }
    public List<Guid> ExistingEngravingIds { get; private set; }
    public List<EngravingDto> NewEngravings { get; private set; }
    
    public List<Guid> ExistingAttachmentIds { get; private set; }
    public List<AttachmentDto> NewAttachments { get; private set; }
}