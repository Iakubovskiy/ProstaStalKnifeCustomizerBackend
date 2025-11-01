using Application.Components.Products.UseCases.RemoveOld;
using Application.Files;
using Domain.Component.Product;
using Domain.Component.Product.Knife;
using Infrastructure.Components;
using Infrastructure.Components.Products;

namespace Application.Components.Products.Knives.UseCases.RemoveOld;

public class RemoveOldUnusedKnivesService : IRemoveOldProduct<Knife>
{
    private readonly IGetOldUnusedProducts<Knife> _getOldUnusedProducts;
    private readonly IComponentRepository<Knife> _componentRepository;
    private readonly IFileService _fileService;

    public RemoveOldUnusedKnivesService(
        IGetOldUnusedProducts<Knife> getOldUnusedProducts, 
        IComponentRepository<Knife> componentRepository,
        IFileService fileService
    )
    {
        this._getOldUnusedProducts = getOldUnusedProducts;
        this._componentRepository = componentRepository;
        this._fileService = fileService;
    }

    public async Task RemoveOldUnusedProducts()
    {
        List<Guid> unusedProductIds = await this._getOldUnusedProducts.GetOldUnusedIds();
        foreach (Guid productId in unusedProductIds)
        {
            await this._componentRepository.Delete(productId);
        }
        
        await this._fileService.RemoveUnusedFiles();
    }
}