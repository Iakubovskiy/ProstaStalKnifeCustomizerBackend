using Application.Components.TexturedComponents.Data;
using Domain;
using Domain.Component;
using Domain.Component.Textures;

namespace Application.Components.SimpleComponents.UseCases.Update;

public interface IUpdateComponent<T, TDto> 
    where T : class, IComponent, IEntity, IUpdatable<T>
{
    public Task<T> Update(Guid id, TDto component);
}