namespace Application.Components.Products.UseCases.RemoveOld;

public interface IRemoveOldProduct<Knife>
{
    public Task RemoveOldUnusedProducts();
}