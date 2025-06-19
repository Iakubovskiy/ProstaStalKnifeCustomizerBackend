namespace Application.Currencies;

public interface IPriceService
{
    public Task<double> GetPrice(double price, string currency);
}