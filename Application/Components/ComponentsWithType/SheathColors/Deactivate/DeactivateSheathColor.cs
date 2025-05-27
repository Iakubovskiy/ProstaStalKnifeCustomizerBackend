using Application.Components.ComponentsWithType.UseCases.Deactivate;
using Domain;
using Domain.Component;
using Domain.Component.Sheaths.Color;
using Infrastructure.Components.Sheaths.Color;

namespace Application.Components.ComponentsWithType.SheathColors.Deactivate;

public class DeactivateSheathColor : IDeactivateSheathColorService
{
    private readonly ISheathColorRepository _sheathColorRepository;

    public DeactivateSheathColor(ISheathColorRepository sheathColorRepository)
    {
        this._sheathColorRepository = sheathColorRepository;
    }
    public async void Deactivate(Guid id)
    {
        SheathColor sheathColor = await this._sheathColorRepository.GetById(id);
        sheathColor.Deactivate();
        await this._sheathColorRepository.Update(id, sheathColor);
    }
}