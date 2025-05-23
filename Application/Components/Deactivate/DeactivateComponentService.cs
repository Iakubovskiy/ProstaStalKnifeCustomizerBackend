using Domain;
using Domain.Component;
using Infrastructure.Components;

namespace Application.Components.Deactivate;

public class DeactivateComponentService<T> : IDeactivate<T> where T: class, IEntity, IComponent,IUpdatable<T>
{
    private readonly IComponentRepository<T> _componentRepository;
    public DeactivateComponentService(
        IComponentRepository<T> componentRepository
    )
    {
        this._componentRepository = componentRepository;
    }
    
    public void Deactivate(T component)
    {
        component.Activate();
        this._componentRepository.Update(component.Id, component);
    }
}