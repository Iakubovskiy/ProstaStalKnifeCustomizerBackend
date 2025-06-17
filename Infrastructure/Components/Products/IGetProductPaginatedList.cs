using Domain.Component.Product;
using Infrastructure.Components.Products.Filters;

namespace Infrastructure.Components.Products;

public interface IGetProductPaginatedList
{
    public Task<PaginatedResult<Product>> GetProductPaginatedList(int pageNumber, int pageSize, string locale, ProductFilters? filters = null);
}