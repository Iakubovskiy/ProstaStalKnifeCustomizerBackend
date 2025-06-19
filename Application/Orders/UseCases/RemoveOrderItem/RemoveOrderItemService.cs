using Infrastructure.Components.Products;
using Infrastructure.Orders;

namespace Application.Orders.UseCases.RemoveOrderItem;

public class RemoveOrderItemService : IRemoveOrderItem
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public RemoveOrderItemService(
        IOrderRepository orderRepository,
        IProductRepository productRepository
    )
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task RemoveOrderItem(Guid orderId, Guid productId)
    {
        var order = await _orderRepository.GetById(orderId)
                    ?? throw new InvalidOperationException("Order not found");

        var product = await _productRepository.GetById(productId)
                      ?? throw new InvalidOperationException("Product not found");

        order.RemoveOrderItem(product);

        await _orderRepository.Update(orderId, order);
    }
}
