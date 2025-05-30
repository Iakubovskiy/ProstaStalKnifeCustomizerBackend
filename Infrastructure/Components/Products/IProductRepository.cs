using Domain.Component.Product;

namespace Infrastructure.Components.Products;

public interface IProductRepository : IComponentRepository<Product>
{
    public Task<List<Product>> GetProductsByIds(List<Guid> ids);
}