using Domain;
using Domain.Component;

namespace Application.Components.ComponentsWithType.UseCases.Deactivate;

public interface IDeactivateSheathColorService
{
    public void Deactivate(Guid id);
}