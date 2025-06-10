namespace Domain.Component.Engravings.Support;

public class EngravingPrice : IEntity, IUpdatable<EngravingPrice>
{
    private EngravingPrice()
    {
        
    }

    public EngravingPrice(Guid id, double price)
    {
        if (price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        this.Id = id;
        this.Price = price;
    }
    
    public Guid Id { get; private set; }
    public double Price { get; private set; }
    
    public void Update(EngravingPrice price)
    {
        if (price.Price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        this.Price = price.Price;
    }
}