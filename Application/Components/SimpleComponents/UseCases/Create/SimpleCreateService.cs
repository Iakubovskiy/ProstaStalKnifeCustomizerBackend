using Domain;
using Infrastructure;

namespace Application.Components.SimpleComponents.UseCases.Create;

public class SimpleCreateService<T, TDto> : ISimpleCreateService<T, TDto>
where T : class, IEntity, IUpdatable<T>
{
    private readonly IRepository<T> _componentRepository;
    private readonly IComponentDtoMapper<T, TDto> _componentDtoMapper;
    
    public SimpleCreateService(
        IRepository<T> componentRepository,
        IComponentDtoMapper<T, TDto> componentDtoMapper
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