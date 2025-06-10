using Domain;

namespace Application.Components.SimpleComponents.UseCases.Update;

public interface ISimpleUpdateService<T, TDto> 
    where T : class, IEntity, IUpdatable<T>
{
    public Task<T> Update(Guid id, TDto component);
}
