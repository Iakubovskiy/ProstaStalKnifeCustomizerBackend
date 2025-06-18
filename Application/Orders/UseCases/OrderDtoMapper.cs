using Application.Orders.Dto;
using Domain.Component.Product;
using Domain.Order;
using Domain.Order.Support;
using Infrastructure.Components.Products;
using Infrastructure.Orders;
using Infrastructure.Orders.Support.DeliveryTypes;
using Infrastructure.Orders.Support.PaymentMethods;

namespace Application.Orders.UseCases;

public class OrderDtoMapper : IOrderDtoMapper
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDeliveryTypeRepository _deliveryTypeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public OrderDtoMapper(
        IOrderRepository orderRepository,
        IDeliveryTypeRepository deliveryTypeRepository,
        IProductRepository productRepository,
        IPaymentMethodRepository paymentMethodRepository
    )
    {
        this._orderRepository = orderRepository;
        this._deliveryTypeRepository = deliveryTypeRepository;
        this._productRepository = productRepository;
        this._paymentMethodRepository = paymentMethodRepository;
    }

    public async Task<Order> Map(OrderDto orderDto)
    {
        Guid id = orderDto.Id ?? Guid.NewGuid();
        int orderNumber = await this._orderRepository.GetLastOrderNumber() + 1;
        DeliveryType deliveryType = await this._deliveryTypeRepository.GetById(orderDto.DeliveryTypeId);
        
        List<Product> products = await this._productRepository.GetProductsByIds(orderDto.ProductIds);
        
        PaymentMethod paymentMethod = await this._paymentMethodRepository.GetById(orderDto.PaymentMethodId);
        
        Order order = new Order(
                id,
                orderNumber,
                orderDto.Total,
                products,
                deliveryType,
                orderDto.ClientData,
                orderDto.Comment,
                orderDto.Status,
                paymentMethod
        );
        
        return order;
    }
}