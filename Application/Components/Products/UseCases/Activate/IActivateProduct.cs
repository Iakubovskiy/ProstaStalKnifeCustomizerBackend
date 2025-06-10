using Domain.Component.Product;

namespace Application.Components.Products.UseCases.Activate;

public interface IActivateProduct<T>
    where T : Product
{
    public Task Activate(Guid id);
}