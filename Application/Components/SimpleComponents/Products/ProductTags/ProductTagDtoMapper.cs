using Application.Components.SimpleComponents.UseCases;
using Domain.Component.Product;
using Domain.Translation;

namespace Application.Components.SimpleComponents.Products.ProductTags;

public class ProductTagDtoMapper : IComponentDtoMapper<ProductTag, ProductTagDto>
{
    public async Task<ProductTag> Map(ProductTagDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations names = new Translations(dto.Name);

        return new ProductTag(id, names);
    }
}