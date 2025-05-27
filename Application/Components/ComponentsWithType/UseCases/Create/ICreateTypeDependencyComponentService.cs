using Domain;
using Domain.Component;

namespace Application.Components.ComponentsWithType.UseCases.Create;

public interface ICreateTypeDependencyComponentService<T, TDto> 
    where T : class, IEntity, IUpdatable<T>, IComponentWithTypeDependency
{
    public Task<T> Create(TDto component);
}