using Domain.Component.Sheaths.Color;

namespace Application.Components.ComponentsWithType.UseCases.Get;

public interface ISheathColorMapper
{
    public Task<SheathColorResponsePresenter> MapToPresenter(SheathColor sheathColor);
    public Task<List<SheathColorResponsePresenter>> MapToPresenter(IEnumerable<SheathColor> sheathColors);
}