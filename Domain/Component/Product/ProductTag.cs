using Domain.Component.Translation;

namespace Domain.Component.Product;

public class ProductTag : IEntity, IUpdatable<ProductTag>
{
    public ProductTag(
        Guid id,
        Translations tag
    )
    {
        this.Id = id;
        this.Tag = tag;
    }
    
    public Guid Id { get; private set; }
    public Translations Tag { get; private set; }
    
    public void Update(ProductTag tag)
    {
        this.Tag = tag.Tag;
    }
}