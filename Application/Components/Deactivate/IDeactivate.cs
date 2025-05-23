using Domain;
using Domain.Component;

namespace Application.Components.Deactivate;

public interface IDeactivate<T> where T: class, IEntity, IComponent,IUpdatable<T>
{
    public void Deactivate(T component);
}