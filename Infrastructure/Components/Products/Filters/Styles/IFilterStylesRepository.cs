namespace Infrastructure.Components.Products.Filters.Styles;

public interface IFilterStylesRepository
{
    public Task<List<string>> GetAllStyleTags(string locale);
}