using Domain.Component.Product;
using Domain.Orders.Support;

namespace Domain.Orders;

public class Order : IEntity, IUpdatable<Order>
{

    private Order()
    {
        
    }

    public Order(
        Guid id, 
        int number, 
        double total, 
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
        this.DeliveryType = deliveryType;
        this.ClientData = clientData;
        this.Comment = comment;
        this.Status = status;
        this.PaymentMethod = paymentMethod;
    }
    
    public Guid Id { get; private set; }
    public int Number { get; private set; }
    public double Total { get; private set; }
    public List<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
    public DeliveryType DeliveryType { get; private set; }
    public ClientData ClientData { get; private set; }
    public string? Comment { get; private set; }
    public string Status { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }

    public void Update(Order order)
    {
        this.Total = order.Total;
        this.OrderItems = order.OrderItems;
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

    public void AddOrderItem(Product product, int quantity)
    {
        OrderItem orderItem = new OrderItem(product, this, quantity);
        this.OrderItems.Add(orderItem);
    }
    
    public void UpdateOrderItemQuantity(Product product, int quantity)
    {
        var orderItem = this.OrderItems.FirstOrDefault(oi => oi.Product.Id == product.Id);
        
        if (orderItem == null)
            throw new InvalidOperationException("Product not found in order");
        
        if (quantity <= 0)
        {
            this.OrderItems.Remove(orderItem);
        }
        else
        {
            orderItem.UpdateQuantity(quantity);
        }
    }

    public void RemoveOrderItem(Product product)
    {
        var orderItem = this.OrderItems.FirstOrDefault(oi => oi.Product.Id == product.Id);
        
        if (orderItem != null)
        {
            this.OrderItems.Remove(orderItem);
        }
    }
}