using Domain.Component.Sheaths.Color;

namespace Infrastructure.Components.Sheaths.Color;

public interface ISheathColorRepository : IRepository<SheathColor>
{
    public Task<List<SheathColor>> GetAllActive();
}