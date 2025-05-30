using Domain;
using Domain.Component.Product;
using Infrastructure.Components;

namespace Application.Components.Products.UseCases.Activate;

public class ActivateProductService<T> : IActivateProduct<T>
where T: Product, IUpdatable<T>
{
    private readonly IComponentRepository<T> _productRepository;

    public ActivateProductService(IComponentRepository<T> productRepository)
    {
        this._productRepository = productRepository;
    }

    public async void Activate(Guid id)
    {
        T product = await this._productRepository.GetById(id);
        product.Activate();
    }
}