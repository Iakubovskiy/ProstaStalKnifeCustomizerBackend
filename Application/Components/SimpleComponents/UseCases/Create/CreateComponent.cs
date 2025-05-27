using Application.Components.TexturedComponents.Data;
using Domain;
using Domain.Component;
using Domain.Component.Textures;
using Infrastructure;
using Infrastructure.Components;

namespace Application.Components.SimpleComponents.UseCases.Create;

public class CreateComponent<T, TDto> : ICreateService<T, TDto> 
    where T : class, ITextured, IComponent, IEntity, IUpdatable<T>
{
    private readonly IComponentRepository<T> _componentRepository;
    private readonly IComponentDtoMapper<T, TDto> _componentDtoMapper;
    
    public CreateComponent(
        IComponentRepository<T> componentRepository,
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