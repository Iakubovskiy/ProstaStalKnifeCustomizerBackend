using Domain;
using Domain.Component;

namespace Infrastructure.Components;

public interface IComponentRepository<T>: IRepository<T> where T : class, IEntity, IUpdatable<T>, IComponent
{
    public Task<List<T>> GetAllActive();
}