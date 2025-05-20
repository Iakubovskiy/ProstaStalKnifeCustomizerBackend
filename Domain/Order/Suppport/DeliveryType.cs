using Domain.Component.Translation;

namespace Domain.Order.Suppport;

public class DeliveryType : IEntity
{
    private DeliveryType()
    {
        
    }

    public DeliveryType(
        Guid id, 
        Translations name, 
        double price, 
        Translations? comment)
    {
        if (price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        this.Id = id;
        this.Name = name;
        this.Price = price;
        this.Comment = comment;
    }
    
    public Guid Id { get; private set; }
    public Translations Name { get; private set; }
    public double Price { get; private set; }
    public Translations? Comment { get; private set; }

    public void Update(DeliveryType deliveryType)
    {
        if (deliveryType.Price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        this.Name = deliveryType.Name;
        this.Price = deliveryType.Price;
        this.Comment = deliveryType.Comment;
    }
}