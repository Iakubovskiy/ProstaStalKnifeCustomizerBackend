using Domain.Component.Sheaths.Color;
using Infrastructure.Components.Sheaths.Color;

namespace Application.Components.ComponentsWithType.SheathColors.Activate;

public class ActivateSheathColor : IActivateSheathColorService
{
    private readonly ISheathColorRepository _sheathColorRepository;

    public ActivateSheathColor(ISheathColorRepository sheathColorRepository)
    {
        this._sheathColorRepository = sheathColorRepository;
    }
    public async Task Activate(Guid id)
    {
        SheathColor sheathColor = await this._sheathColorRepository.GetById(id);
        sheathColor.Activate();
        await this._sheathColorRepository.Update(id, sheathColor);
    }
}