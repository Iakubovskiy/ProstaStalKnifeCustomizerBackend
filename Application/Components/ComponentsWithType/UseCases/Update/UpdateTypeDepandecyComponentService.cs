using Application.Components.SimpleComponents.UseCases;
using Domain;
using Domain.Component;
using Infrastructure.Components;

namespace Application.Components.ComponentsWithType.UseCases.Update;

public class UpdateTypeDependencyComponentService<T, TDto> : IUpdateTypeDependencyComponentService<T, TDto> 
    where T : class, IComponent, IEntity, IUpdatable<T>, IComponentWithTypeDependency
{
    private readonly IComponentRepository<T> _componentRepository;
    private readonly IComponentWithTypeDtoMapper<T, TDto> _componentDtoMapper;
    
    public UpdateTypeDependencyComponentService(
        IComponentRepository<T> componentRepository,
        IComponentWithTypeDtoMapper<T, TDto> componentDtoMapper
    )
    {
        this._componentRepository = componentRepository;
        this._componentDtoMapper = componentDtoMapper;
    }
    
    public virtual async Task<T> Update(Guid id, TDto component)
    {
        var entity = await this._componentDtoMapper.Map(component);
        await this._componentRepository.Update(id,entity);
        
        return entity;
    }
}