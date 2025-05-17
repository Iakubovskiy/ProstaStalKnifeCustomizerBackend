using Domain.Component.Translation;

namespace Domain.Component.Sheath;

public class Sheath : IEntity
{
    public Guid Id { get; }
    public Translations Name { get; private set; }
    public string? ModelUrl { get; private set; }
    public BladeShapeType.BladeShapeType Type { get; private set; }
    public double Price { get; private set; }
    
    public Sheath(Guid id, Translations name, string? modelUrl, BladeShapeType.BladeShapeType type, double price)
    {
        Id = id;
        Name = name;
        ModelUrl = modelUrl;
        Type = type;
        Price = price;
    }

    private Sheath()
    {
        
    }
    
    public double GetPrice()
    {
        return this.Price;
    }
}