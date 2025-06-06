using Domain.Currencies;
using Infrastructure.Currencies;

namespace Infrastructure.Data.Currencies;

public class CurrencySeeder : ISeeder
{
    public int Priority => 0;
    private readonly ICurrencyRepository _currencyRepository;
    
    public CurrencySeeder(ICurrencyRepository currencyRepository)
    {
        this._currencyRepository = currencyRepository;
    }
    
    public async Task SeedAsync()
    {
        int count = (await this._currencyRepository.GetAll()).Count;
        if (count > 0)
        {
            return;
        }
        
        Currency currency1 = new Currency(
            new Guid("44b659d8-08a7-48c0-9748-573f5d600739"),
            "uah",
            1.0
        );
        
        Currency currency2 = new Currency(
            new Guid("5c5da2f8-8109-4555-9738-0b8a7805323b"),
            "usd",
            41.0 
        );
        
        Currency currency3 = new Currency(
            new Guid("0e8c3005-d27f-4252-b764-8e120a2b9f79"),
            "eur",
            44.0
        );
        
        await this._currencyRepository.Create(currency1);
        await this._currencyRepository.Create(currency2);
        await this._currencyRepository.Create(currency3);
    }
}