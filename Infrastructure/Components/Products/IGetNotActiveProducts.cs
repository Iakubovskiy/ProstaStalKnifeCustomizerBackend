namespace Infrastructure.Components.Products;

public interface IGetNotActiveProducts<T>
{
    public Task<List<T>> GetNotActiveProducts();
}