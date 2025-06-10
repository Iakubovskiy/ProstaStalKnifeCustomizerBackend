using Domain.Component.Engravings.Support;
using Domain.Currencies;
using Infrastructure;
using Infrastructure.Currencies;

namespace Application.Components.Prices.Engravings;

public class GetEngravingPriceService: IGetEngravingPrice
{
    private readonly IRepository<EngravingPrice> _engravingPriceRepository;
    private readonly ICurrencyRepository _currencyRepository;
    public GetEngravingPriceService(
        IRepository<EngravingPrice> engravingPriceRepository,
        ICurrencyRepository currencyRepository    
    )
    {
        this._engravingPriceRepository = engravingPriceRepository;
        this._currencyRepository = currencyRepository;
    }
    
    public async Task<EngravingPrice> GetPrice(string currency)
    {
        var engravingPriceList = await this._engravingPriceRepository.GetAll();
        if (engravingPriceList.Count == 0)
        {
            throw new Exception("No engraving price found");
        }
        Currency currencyObject = await this._currencyRepository.GetByName(currency);
        EngravingPrice engravingPriceWithCurrency = new EngravingPrice(
            engravingPriceList.First().Id,
            engravingPriceList.First().Price / currencyObject.ExchangeRate
        );
        engravingPriceList.First().Update(engravingPriceWithCurrency);
        return engravingPriceList.First();
    }
}