using Infrastructure.Components.Products;
using Infrastructure.Orders;

namespace Application.Orders.UseCases.UpdateOrderItemQuantity;

public class UpdateOrderItemQuantityService : IUpdateOrderItemQuantityService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public UpdateOrderItemQuantityService(
        IOrderRepository orderRepository,
        IProductRepository productRepository
    )
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task UpdateQuantity(Guid orderId, Guid productId, int quantity)
    {
        var order = await _orderRepository.GetById(orderId)
                    ?? throw new InvalidOperationException("Order not found");

        var product = await _productRepository.GetById(productId)
                      ?? throw new InvalidOperationException("Product not found");

        order.UpdateOrderItemQuantity(product, quantity);

        await _orderRepository.Update(orderId, order);
    }
}
