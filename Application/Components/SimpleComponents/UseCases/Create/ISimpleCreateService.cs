using Domain;

namespace Application.Components.SimpleComponents.UseCases.Create;

public interface ISimpleCreateService<T, TDto> 
    where T : class, IEntity, IUpdatable<T>
{
    public Task<T> Create(TDto component);
}