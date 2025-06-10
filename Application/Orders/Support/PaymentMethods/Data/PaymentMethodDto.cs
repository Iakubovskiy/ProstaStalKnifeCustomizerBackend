namespace Application.Orders.Support.PaymentMethods.Data;

public class PaymentMethodDto
{
    public Guid? Id { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public Dictionary<string, string> Description { get; set; }
    public bool IsActive { get; set; }
}