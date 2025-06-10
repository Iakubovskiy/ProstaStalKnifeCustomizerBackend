namespace Application.Orders.Support.DeliveryTypes.Data;

public class DeliveryTypeDto
{
    public Guid? Id { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public Dictionary<string, string>? Comment { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
}