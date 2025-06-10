using Domain;
using Domain.Component;
using Infrastructure.Components;

namespace Application.Components.SimpleComponents.UseCases.Update;

public class UpdateService<T, TDto> : IUpdateService<T, TDto> 
    where T : class, IComponent, IEntity, IUpdatable<T>
{
    private readonly IComponentRepository<T> _componentRepository;
    private readonly IComponentDtoMapper<T, TDto> _componentDtoMapper;
    
    public UpdateService(
        IComponentRepository<T> componentRepository,
        IComponentDtoMapper<T, TDto> componentDtoMapper
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