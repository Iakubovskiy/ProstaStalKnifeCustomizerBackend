using Domain.Component.Product;

namespace Application.Components.Products.UseCases.Create;

public interface ICreateProductService<T, TDto> 
where T : Product
where TDto : class, IProductDto
{
    public Task<T> Create(TDto product);
}