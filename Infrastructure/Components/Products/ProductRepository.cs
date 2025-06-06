using Domain.Component.Product;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Products;

public class ProductRepository : ComponentRepository<Product>, IProductRepository
{
    public ProductRepository(DBContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetProductsByIds(List<Guid> ids)
    {
        List<Product> products = await this.Set.Where(p => ids.Contains(p.Id)).ToListAsync();
        return products;
    }
}