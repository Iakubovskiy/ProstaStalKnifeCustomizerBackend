using Application.Components.Products.Attachemts;
using Application.Components.SimpleComponents.Engravings;

namespace Application.Components.Products.Knives;

public class KnifeDto : IProductDto
{
    public Guid? Id { get; set; }
    public bool IsActive { get; set; }
    public Guid ImageFileId { get; set; }
    public Dictionary<string, string> Name { get; set; }
    public Dictionary<string, string> Title { get; set; }
    public Dictionary<string, string> Description { get; set; }
    public Dictionary<string, string> MetaTitle { get; set; }
    public Dictionary<string, string> MetaDescription { get; set; }
    public List<Guid> TagsIds { get; set; }
    
    public Guid ShapeId { get; set; }
    public Guid BladeCoatingColorId { get; set; }
    public Guid? HandleId { get; set; }
    public Guid? SheathId { get; set; }
    public Guid? SheathColorId { get; set; }
    public List<Guid> ExistingEngravingIds { get; set; }
    public List<EngravingDto> NewEngravings { get; set; }
    
    public List<Guid> ExistingAttachmentIds { get; set; }
    public List<AttachmentDto> NewAttachments { get; set; }
}