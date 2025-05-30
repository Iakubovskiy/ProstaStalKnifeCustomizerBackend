using Domain;
using Domain.Component.Product;
using Infrastructure.Components;

namespace Application.Components.Products.UseCases.Update;

public class UpdateProductService<T, TDto> : IUpdateProductService<T, TDto>
where T : Product, IUpdatable<T>
where TDto : class, IProductDto
{
    private readonly IProductDtoMapper<T, TDto> _productMapper;
    private readonly IComponentRepository<T> _productRepository;

    public UpdateProductService(
        IProductDtoMapper<T, TDto> productMapper,
        IComponentRepository<T> productRepository
    )
    {
        this._productMapper = productMapper;
        this._productRepository = productRepository;
    }
    public async Task<T> Update(Guid id, TDto productDto)
    {
        T product = await this._productMapper.Map(productDto);
        return await this._productRepository.Update(id,product);
    }
}