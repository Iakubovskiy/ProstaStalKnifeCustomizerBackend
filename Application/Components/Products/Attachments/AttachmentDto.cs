namespace Application.Components.Products.Attachments;

public class AttachmentDto : IProductDto
{
    public Guid? Id { get; set; }
    public bool IsActive { get; set; }
    public Guid ImageFileId { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public Dictionary<string, string> Titles { get; set; }
    public Dictionary<string, string> Descriptions { get; set; }
    public Dictionary<string, string> MetaTitles { get; set; }
    public Dictionary<string, string> MetaDescriptions { get; set; }
    public List<Guid> TagsIds { get; set; }
    
    public Guid TypeId { get; set; }
    public Dictionary<string, string> Colors { get; set; }
    public double Price { get;   set; }
    public Dictionary<string, string> Materials { get; set; }
    public Guid ModelFileId { get; set; }
}