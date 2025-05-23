using Domain;
using Domain.Component;
using Infrastructure.Components;

namespace Application.Components.Activate;

public class ActivateComponentService<T>: IActivate<T> where T: class, IEntity, IComponent,IUpdatable<T> 
{
    private readonly IComponentRepository<T> _componentRepository;
    public ActivateComponentService(
        IComponentRepository<T> componentRepository
    )
    {
        this._componentRepository = componentRepository;
    }
    
    public void Activate(T component)
    {
        component.Activate();
        this._componentRepository.Update(component.Id, component);
    }
}