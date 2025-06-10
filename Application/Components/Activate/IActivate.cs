using Domain;
using Domain.Component;

namespace Application.Components.Activate;

public interface IActivate<T> where T: class, IEntity, IComponent,IUpdatable<T>
{
    public Task Activate(Guid id);
}