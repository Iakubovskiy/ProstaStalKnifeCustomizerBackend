using Domain.Component;
using Domain.Currencies;
using Infrastructure.Currencies;

namespace Application.Components.Prices;

public class GetComponentPriceService : IGetComponentPrice
{
    private readonly ICurrencyRepository _currencyRepository;

    public GetComponentPriceService(ICurrencyRepository currencyRepository)
    {
        this._currencyRepository = currencyRepository;
    }
    
    public async Task<double> GetPrice(IComponent component, string currencyName)
    {
        Currency currency = await this._currencyRepository.GetByName(currencyName);
        return component.GetPrice(currency.ExchangeRate);
    }
}