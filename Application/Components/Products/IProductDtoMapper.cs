using Domain.Component.Product;

namespace Application.Components.Products;

public interface IProductDtoMapper<T, TDto>
where T : Product
where TDto : class, IProductDto
{
     public Task<T> Map(TDto product);
}