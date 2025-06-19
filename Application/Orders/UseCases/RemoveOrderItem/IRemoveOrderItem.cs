namespace Application.Orders.UseCases.RemoveOrderItem;

public interface IRemoveOrderItem
{
    public Task RemoveOrderItem(Guid orderId, Guid productId);
}