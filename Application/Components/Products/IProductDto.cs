using Domain;
using Domain.Component.Product;

namespace Application.Components.Products;

public interface IProductDto
{
    public Guid? Id { get; }
    public bool IsActive { get; }
    public Guid ImageFileId { get; }
    public Dictionary<string, string> Name { get; }
    public Dictionary<string, string> Title { get; }
    public Dictionary<string, string> Description { get; }
    public Dictionary<string, string> MetaTitle { get; }
    public Dictionary<string, string> MetaDescription { get; }
    public List<Guid> TagsIds { get; }
}