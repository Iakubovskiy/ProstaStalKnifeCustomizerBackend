namespace Infrastructure.Components.Products;

public interface IGetOldUnusedProducts<T>
{
    public Task<List<T>> GetOldUnusedProducts();
    public Task<List<Guid>> GetOldUnusedIds();
}