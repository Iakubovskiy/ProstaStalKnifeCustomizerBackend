using Application.Components.TexturedComponents.Data;
using Domain;
using Domain.Component;
using Domain.Component.Textures;

namespace Application.Components.SimpleComponents.UseCases.Create;

public interface ICreateSimpleComponent<T, TDto> 
    where T : class, IComponent, IEntity, IUpdatable<T>
{
    public Task<T> Create(TDto component);
}