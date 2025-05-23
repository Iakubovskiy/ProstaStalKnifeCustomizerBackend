using Domain.Component;

namespace Application.Components.Prices;

public interface IGetPrice
{
    public Task<double> GetPrice(IComponent component ,string currency);
}