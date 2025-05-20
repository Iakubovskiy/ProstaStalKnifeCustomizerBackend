namespace Domain.Component.Engravings.Support;

public class EngravingPrice : IEntity
{
    private EngravingPrice()
    {
        
    }

    public EngravingPrice(Guid id, double price)
    {
        this.Id = id;
        this.Price = price;
    }
    
    public Guid Id { get; private set; }
    public double Price { get; set; }
}