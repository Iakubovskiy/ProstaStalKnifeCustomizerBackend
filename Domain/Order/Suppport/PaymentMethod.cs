using Domain.Component.Translation;

namespace Domain.Order.Suppport;

public class PaymentMethod : IEntity, IUpdatable<PaymentMethod>
{
    private PaymentMethod()
    {
        
    }

    public PaymentMethod(
        Guid id, 
        Translations name, 
        Translations description
    )
    {
        Id = id;
        Name = name;
        Description = description;
    }
    
    public Guid Id { get; set; }
    public Translations Name { get; set; }
    public Translations Description { get; set; }

    public void Update(PaymentMethod paymentMethod)
    {
        this.Name = paymentMethod.Name;
        this.Description = paymentMethod.Description;
    }
}