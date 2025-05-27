using Domain.Component;

namespace Application.Components.Prices;

public interface IGetComponentPrice
{
    public Task<double> GetPrice(IComponent component ,string currency);
}