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
    
    public async Task Deactivate(Guid id)
    {
        T component = await this._componentRepository.GetById(id);
        component.Deactivate();
        await this._componentRepository.Update(id, component);
    }
}