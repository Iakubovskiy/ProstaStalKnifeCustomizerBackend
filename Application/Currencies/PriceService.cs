using Domain.Currencies;
using Infrastructure.Currencies;

namespace Application.Currencies;

public class PriceService : IPriceService
{
    private readonly ICurrencyRepository _currencyRepository;

    public PriceService(ICurrencyRepository currencyRepository)
    {
        this._currencyRepository = currencyRepository;
    }
    
    public async Task<double> GetPrice(double price, string currencyName)
    {
        Currency currency = await this._currencyRepository.GetByName(currencyName);
        return price/currency.ExchangeRate;
    }
}