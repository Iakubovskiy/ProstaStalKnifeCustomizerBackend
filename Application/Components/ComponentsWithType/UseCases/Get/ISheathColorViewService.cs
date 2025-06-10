namespace Application.Components.ComponentsWithType.UseCases.Get;

public interface ISheathColorViewService
{
    Task<List<SheathColorResponsePresenter>> GetAllAsync();
    Task<List<SheathColorResponsePresenter>> GetAllActiveAsync();
    Task<SheathColorResponsePresenter> GetByIdAsync(Guid id);
}