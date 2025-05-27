using Domain.Translation;

namespace Domain.Order.Support;

public class PaymentMethod : IEntity, IUpdatable<PaymentMethod>
{
    private PaymentMethod()
    {
        
    }

    public PaymentMethod(
        Guid id, 
        Translations name, 
        Translations description,
        bool isActive
    )
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
    }
    
    public Guid Id { get; set; }
    public Translations Name { get; set; }
    public Translations Description { get; set; }
    public bool IsActive { get; set; }

    public void Update(PaymentMethod paymentMethod)
    {
        this.Name = paymentMethod.Name;
        this.Description = paymentMethod.Description;
        this.IsActive = paymentMethod.IsActive;
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