using Application.Components.SimpleComponents.UseCases;
using Domain;
using Domain.Component;
using Domain.Component.Textures;
using Infrastructure.Components;

namespace Application.Components.ComponentsWithType.UseCases.Create;

public class CreateTypeDependencyComponentService<T, TDto> : ICreateTypeDependencyComponentService<T, TDto> 
    where T : class, ITextured, IComponent, IEntity, IUpdatable<T>, IComponentWithTypeDependency
{
    private readonly IComponentRepository<T> _componentRepository;
    private readonly IComponentWithTypeDtoMapper<T, TDto> _componentDtoMapper;
    
    public CreateTypeDependencyComponentService(
        IComponentRepository<T> componentRepository,
        IComponentWithTypeDtoMapper<T, TDto> componentDtoMapper
    )
    {
        this._componentRepository = componentRepository;
        this._componentDtoMapper = componentDtoMapper;
    }
    
    public virtual async Task<T> Create(TDto component)
    {
        
        var entity = await this._componentDtoMapper.Map(component);
        await this._componentRepository.Create(entity);
        
        return entity;
    }
}