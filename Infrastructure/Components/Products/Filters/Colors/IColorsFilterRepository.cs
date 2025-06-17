namespace Infrastructure.Components.Products.Filters.Colors;

public interface IColorsFilterRepository
{
    public Task<List<string>> GetAllColorFilters(string locale);
}