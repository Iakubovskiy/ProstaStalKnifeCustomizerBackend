using Domain;
using Domain.Component.Product;
using Infrastructure.Components;

namespace Application.Components.Products.UseCases.Deactivate;

public class DeactivateProductService<T> : IDeactivateProduct<T>
where T: Product, IUpdatable<T>
{
    private readonly IComponentRepository<T> _productRepository;

    public DeactivateProductService(IComponentRepository<T> productRepository)
    {
        this._productRepository = productRepository;
    }

    public async void Deactivate(Guid id)
    {
        T product = await this._productRepository.GetById(id);
        product.Deactivate();
    }
}