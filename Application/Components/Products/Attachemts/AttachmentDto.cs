namespace Application.Components.Products.Attachemts;

public class AttachmentDto : IProductDto
{
    public AttachmentDto(
        Guid? id, 
        bool isActive, 
        Guid imageFileId, 
        Dictionary<string, string> name, 
        Dictionary<string, string> title, 
        Dictionary<string, string> description, 
        Dictionary<string, string> metaTitle, 
        Dictionary<string, string> metaDescription, 
        List<Guid> tagsIds, 
        Guid typeId, 
        Dictionary<string, string> color,
        double price, 
        Dictionary<string, string> material, 
        Guid modelFileId
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
        TypeId = typeId;
        Color = color;
        Price = price;
        Material = material;
        ModelFileId = modelFileId;
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
    
    public Guid TypeId { get; private set; }
    public Dictionary<string, string> Color { get; private set; }
    public double Price { get; private set; }
    public Dictionary<string, string> Material { get; private set; }
    public Guid ModelFileId { get; private set; }
}