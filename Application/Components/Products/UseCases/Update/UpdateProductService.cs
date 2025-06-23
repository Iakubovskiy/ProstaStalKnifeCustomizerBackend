using System.Data.Entity.Core;
using Domain;
using Domain.Component.Engravings;
using Domain.Component.Product;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Infrastructure.Components;

namespace Application.Components.Products.UseCases.Update;

public class UpdateProductService<T, TDto> : IUpdateProductService<T, TDto>
where T : Product, IUpdatable<T>
where TDto : class, IProductDto
{
    private readonly IProductDtoMapper<T, TDto> _productMapper;
    private readonly IComponentRepository<T> _productRepository;
    private readonly IComponentRepository<Engraving> _engravingRepository;

    public UpdateProductService(
        IProductDtoMapper<T, TDto> productMapper,
        IComponentRepository<T> productRepository,
        IComponentRepository<Engraving> engravingRepository
    )
    {
        this._productMapper = productMapper;
        this._productRepository = productRepository;
        this._engravingRepository = engravingRepository;
    }
    public async Task<T> Update(Guid id, TDto productDto)
    {
        T product = await this._productMapper.Map(productDto);
        switch (product)
        {
            case Knife knife:
            {
                if (knife.Engravings != null)
                {
                    foreach (Engraving engraving in knife.Engravings)
                    {
                        try
                        {
                            var existing = await _engravingRepository.GetById(engraving.Id);
                        }
                        catch (ObjectNotFoundException e)
                        {
                            await this._engravingRepository.Create(engraving);
                        }
                    }
                }

                break;
            }
            case CompletedSheath sheath:
            {
                if (sheath.Engravings != null)
                {
                    foreach (Engraving engraving in sheath.Engravings)
                    {
                        try
                        {
                            var existing = await _engravingRepository.GetById(engraving.Id);
                        }
                        catch (ObjectNotFoundException e)
                        {
                            await this._engravingRepository.Create(engraving);
                        }
                    }
                }

                break;
            }
        }
        return await this._productRepository.Update(id,product);
    }
}