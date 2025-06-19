using Domain.Translation;

namespace Domain.Orders.Support;

public class DeliveryType : IEntity, IUpdatable<DeliveryType>
{
    private DeliveryType()
    {
        
    }

    public DeliveryType(
        Guid id, 
        Translations name, 
        double price, 
        Translations? comment,
        bool isActive
    )
    {
        if (price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        this.Id = id;
        this.Name = name;
        this.Price = price;
        this.Comment = comment;
        this.IsActive = isActive;
    }
    
    public Guid Id { get; private set; }
    public Translations Name { get; private set; }
    public double Price { get; private set; }
    public Translations? Comment { get; private set; }
    public bool IsActive { get; set; }

    public void Update(DeliveryType deliveryType)
    {
        if (deliveryType.Price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        this.Name = deliveryType.Name;
        this.Price = deliveryType.Price;
        this.Comment = deliveryType.Comment;
        this.IsActive = deliveryType.IsActive;
    }

    public void Activate()
    {
        this.IsActive = true;
    }
    public void Deactivate()
    {
        this.IsActive = false;
    }
}