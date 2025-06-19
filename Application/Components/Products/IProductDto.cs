using Domain;
using Domain.Component.Product;

namespace Application.Components.Products;

public interface IProductDto
{
    public Guid? Id { get; }
    public bool IsActive { get; }
    public Guid ImageFileId { get; }
    public Dictionary<string, string> Names { get; }
    public Dictionary<string, string> Titles { get; }
    public Dictionary<string, string> Descriptions { get; }
    public Dictionary<string, string> MetaTitles { get; }
    public Dictionary<string, string> MetaDescriptions { get; }
    public List<Guid> TagsIds { get; }
}