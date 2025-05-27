namespace Application.Orders.Support.PaymentMethods.Data;

public class PaymentMethodDto
{
    private PaymentMethodDto(
        string namesJson,
        string descriptionJson,
        bool isActive,
        Guid? id = null    
    )
    {
        this.NamesJson = namesJson;
        this.DescriptionJson = descriptionJson;
        this.IsActive = isActive;
        this.Id = id;
    }
    public Guid? Id { get; private set; }
    public string NamesJson { get; private set; }
    public string DescriptionJson { get; private set; }
    public bool IsActive { get; private set; }
}