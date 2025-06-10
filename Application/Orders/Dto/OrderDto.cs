using Domain.Order.Support;

namespace Application.Orders.Dto;

public class OrderDto
{
    public OrderDto(
        Guid? id, 
        List<Guid> productIds, 
        double total, 
        Guid deliveryTypeId, 
        ClientData clientData, 
        string? comment, 
        string status, 
        Guid paymentMethodId
    )
    {
        this.Id = id;
        this.ProductIds = productIds;
        this.Total = total;
        this.DeliveryTypeId = deliveryTypeId;
        this.ClientData = clientData;
        this.Comment = comment;
        this.Status = status;
        this.PaymentMethodId = paymentMethodId;
    }

    public Guid? Id { get; private set; }
    public List<Guid> ProductIds { get; private set; }
    public double Total  { get; private set; }
    public Guid DeliveryTypeId { get; private set; }
    public ClientData ClientData { get; private set; }
    public string? Comment { get; private set; }
    public string Status { get; private set; }
    public Guid PaymentMethodId { get; private set; }
}