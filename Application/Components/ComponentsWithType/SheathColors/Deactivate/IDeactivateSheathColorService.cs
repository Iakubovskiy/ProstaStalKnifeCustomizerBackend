using Domain;
using Domain.Component;

namespace Application.Components.ComponentsWithType.UseCases.Deactivate;

public interface IDeactivateSheathColorService
{
    public Task Deactivate(Guid id);
}