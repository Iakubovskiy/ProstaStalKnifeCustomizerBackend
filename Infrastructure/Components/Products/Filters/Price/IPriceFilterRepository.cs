namespace Infrastructure.Components.Products.Filters.Price;

public interface IPriceFilterRepository
{
    public Task<(double, double)> GetPriceFilter(string currency);
}