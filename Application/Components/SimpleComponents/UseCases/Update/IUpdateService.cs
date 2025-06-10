using Application.Components.TexturedComponents.Data;
using Domain;
using Domain.Component;
using Domain.Component.Textures;

namespace Application.Components.SimpleComponents.UseCases.Update;

public interface IUpdateService<T, TDto> 
    where T : class, IEntity, IUpdatable<T>, IComponent
{
    public Task<T> Update(Guid id, TDto component);
}