using Domain;
using Domain.Component;

namespace Application.Components.ComponentsWithType.UseCases.Update;

public interface IUpdateTypeDependencyComponentService<T, TDto> 
    where T : class, IEntity, IUpdatable<T>, IComponentWithTypeDependency
{
    public Task<T> Update(Guid id, TDto component);
}