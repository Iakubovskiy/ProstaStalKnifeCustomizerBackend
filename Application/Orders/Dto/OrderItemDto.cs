namespace Application.Orders.Dto;

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    
    public OrderItemDto(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}