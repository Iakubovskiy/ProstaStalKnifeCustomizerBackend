using Domain.Component.Product;
using Infrastructure.Data;

namespace Infrastructure.Components.Products;

public class ProductRepository : ComponentRepository<Product>, IProductRepository
{
    public ProductRepository(DBContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetProductsByIds(List<Guid> ids)
    {
        List<Product> products = new List<Product>();
        foreach (Guid id in ids)
        {
            products.Add(await this.GetById(id));
        }
        return products;
    }
}