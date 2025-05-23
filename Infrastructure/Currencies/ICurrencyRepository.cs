using Domain.Currencies;
namespace Infrastructure.Currencies;

public interface ICurrencyRepository : IRepository<Currency>
{
    public Task<Currency> GetByName(string name);
}