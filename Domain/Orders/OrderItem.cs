using System.Text.Json.Serialization;
using Domain.Component.Product;

namespace Domain.Orders;

public class OrderItem
{
    private OrderItem()
    {
        
    }
    
    public OrderItem(
        Product product, 
        Order order, 
        int quantity
    )
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than 0");
        }
        this.Product = product;
        this.Order = order;
        this.Quantity = quantity;
    }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Product Product { get; set; }
    [JsonIgnore] 
    public Order Order { get; set; }
    public int Quantity { get; set; }

    public void Update(OrderItem orderItem)
    {
        if (orderItem.Quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than 0");
        }
        this.Quantity = orderItem.Quantity;
        this.Product = orderItem.Product;
        this.Order = orderItem.Order;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than 0");
        }
        this.Quantity = quantity;
    }
}