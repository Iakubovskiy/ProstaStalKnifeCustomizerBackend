using Domain.Component.Product;

namespace Domain.Order;

public class OrderItem
{
    private OrderItem()
    {
        
    }
    
    public OrderItem(
        Product product, 
        Order order, 
        int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than 0");
        }
        this.Product = product;
        this.Order = order;
        this.Quantity = quantity;
    }
    
    public Product Product { get; set; }
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