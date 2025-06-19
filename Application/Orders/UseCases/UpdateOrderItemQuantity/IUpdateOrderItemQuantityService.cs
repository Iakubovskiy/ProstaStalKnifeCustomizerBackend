namespace Application.Orders.UseCases.UpdateOrderItemQuantity;

public interface IUpdateOrderItemQuantityService
{
    public Task UpdateQuantity(Guid orderId, Guid productId, int quantity);
}