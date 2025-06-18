using Domain.Component.Product;
using Domain.Order.Support;

namespace Domain.Order;

public class Order : IEntity, IUpdatable<Order>
{

    private Order()
    {
        
    }

    public Order(
        Guid id, 
        int number, 
        double total, 
        List<Product> products, 
        DeliveryType deliveryType,
        ClientData clientData,
        string? comment, 
        string status, 
        PaymentMethod paymentMethod)
    {
        if (!Enum.IsDefined(typeof(OrderStatuses), status))
        {
            throw new ArgumentException("Status is not valid");
        }
        this.Id = id;
        this.Number = number;
        this.Total = total;
        this.Products = products;     
        this.DeliveryType = deliveryType;
        this.ClientData = clientData;
        this.Comment = comment;
        this.Status = status;
        this.PaymentMethod = paymentMethod;
    }
    
    public Guid Id { get; private set; }
    public int Number { get; private set; }
    public double Total { get; private set; }
    public List<Product> Products { get; private set; }
    public DeliveryType DeliveryType { get; private set; }
    public ClientData ClientData { get; private set; }
    public string? Comment { get; private set; }
    public string Status { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }

    public void Update(Order order)
    {
        this.Total = order.Total;
        this.Products = order.Products;
        this.DeliveryType = order.DeliveryType;
        this.ClientData = order.ClientData;
        this.Comment = order.Comment;
        this.Status = order.Status;
        this.PaymentMethod = order.PaymentMethod;
    }

    public void ChangeClientData(ClientData clientData)
    {
        this.ClientData = clientData;
    }

    public void ChangeStatus(string status)
    {
        if (!Enum.IsDefined(typeof(OrderStatuses), status))
        {
            throw new ArgumentException("Status is not valid");
        }
        this.Status = status;
    }
}