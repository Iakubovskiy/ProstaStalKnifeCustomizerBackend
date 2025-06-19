namespace Application.Components.SimpleComponents.Products.ProductTags;

public class ProductTagDto
{
    public Guid? Id { get; set; }
    public Dictionary<string, string> Names { get; set; }
}