using Domain.Component.Engravings.Support;

namespace Application.Components.Prices.Engravings;

public interface IGetEngravingPrice
{
    public Task<EngravingPrice> GetPrice(string currency);
}