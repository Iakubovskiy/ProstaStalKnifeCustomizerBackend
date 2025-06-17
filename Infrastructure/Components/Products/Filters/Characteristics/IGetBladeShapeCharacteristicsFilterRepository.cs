namespace Infrastructure.Components.Products.Filters.Characteristics;

public interface IGetBladeShapeCharacteristicsFilterRepository
{
    public Task<(double, double)> GetBladeLengthFilters();
    public Task<(double, double)> GetTotalLengthFilters();
    public Task<(double, double)> GetBladeWidthFilters();
    public Task<(double, double)> GetBladeWeightFilters();
}