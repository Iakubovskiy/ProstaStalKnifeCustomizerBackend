using Infrastructure.Components.Sheaths.Color;

namespace Application.Components.ComponentsWithType.UseCases.Get;

public class SheathColorViewService : ISheathColorViewService
{
    private readonly ISheathColorRepository _sheathColorRepository;
    private readonly ISheathColorMapper _mapper;

    public SheathColorViewService(
        ISheathColorRepository sheathColorRepository,
        ISheathColorMapper mapper
    )
    {
        this._sheathColorRepository = sheathColorRepository;
        this._mapper = mapper;
    }

    public async Task<List<SheathColorResponsePresenter>> GetAllAsync()
    {
        var sheathColors = await this._sheathColorRepository.GetAll();
        return await this._mapper.MapToPresenter(sheathColors);
    }

    public async Task<List<SheathColorResponsePresenter>> GetAllActiveAsync()
    {
        var activeSheathColors = await this._sheathColorRepository.GetAllActive();
        return await this._mapper.MapToPresenter(activeSheathColors);
    }

    public async Task<SheathColorResponsePresenter> GetByIdAsync(Guid id)
    {
        var sheathColor = await this._sheathColorRepository.GetById(id);
        return await this._mapper.MapToPresenter(sheathColor);
    }
}