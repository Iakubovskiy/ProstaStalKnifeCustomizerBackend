namespace Application.Components.Products.Attachemts;

public class AttachmentDto : IProductDto
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
    
    public Guid TypeId { get; set; }
    public Dictionary<string, string> Color { get; set; }
    public double Price { get;   set; }
    public Dictionary<string, string> Material { get; set; }
    public Guid ModelFileId { get; set; }
}