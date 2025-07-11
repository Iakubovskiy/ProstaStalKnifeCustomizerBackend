using Domain.Component.Product;

namespace Application.Components.Products.UseCases.Deactivate;

public interface IDeactivateProduct<T>
where T : Product
{
    public Task Deactivate(Guid id);
}