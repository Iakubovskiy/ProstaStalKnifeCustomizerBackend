namespace Domain.Currencies;

public class Currency : IEntity, IUpdatable<Currency>
{
    private Currency()
    {
        
    }
    public Currency(
        Guid id,
        string name,
        double exchangeRate
    )
    {
        this.Id = id;
        this.Name = name.ToLower();
        if (exchangeRate <= 0)
        {
            throw new ArgumentException("Exchange rate must be greater than 0");
        }
        this.ExchangeRate = exchangeRate;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public double ExchangeRate { get; private set; }

    public void Update(Currency currency)
    {
        this.Name = currency.Name;
        this.ExchangeRate = currency.ExchangeRate;
    }
}