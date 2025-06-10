using Domain.Component.Product;

namespace Application.Components.Products.UseCases.Update;

public interface IUpdateProductService<T, TDto> 
where T : Product
where TDto : class, IProductDto
{
    public Task<T> Update(Guid id, TDto product);
}