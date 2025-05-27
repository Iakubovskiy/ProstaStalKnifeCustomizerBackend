namespace Application.Components.SimpleComponents.Products.ProductTags;

public class ProductTagDto
{
    public ProductTagDto(
        Dictionary<string, string> name,
        Guid? id = null
    )
    {
        this.Name = name;
        this.Id = id;
    }
    
    public Guid? Id { get; private set; }
    public Dictionary<string, string> Name { get; private set; }
}