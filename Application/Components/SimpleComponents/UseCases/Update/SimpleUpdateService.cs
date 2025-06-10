using Domain;
using Infrastructure;

namespace Application.Components.SimpleComponents.UseCases.Update;

public class SimpleUpdateService<T, TDto> : ISimpleUpdateService<T, TDto> 
    where T : class, IEntity, IUpdatable<T>
{
    private readonly IRepository<T> _componentRepository;
    private readonly IComponentDtoMapper<T, TDto> _componentDtoMapper;
    
    public SimpleUpdateService(
        IRepository<T> componentRepository,
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