using Domain.Component.Product;
using Domain.Order;
using Domain.Order.Support;
using Infrastructure.Components.Products;
using Infrastructure.Orders;
using Infrastructure.Orders.Support.DeliveryTypes;
using Infrastructure.Orders.Support.PaymentMethods;

namespace Application.Orders.Dto;

public class OrderDtoMapper : IOrderDtoMapper
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDeliveryTypeRepository _deliveryTypeRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly IProductRepository _productRepository;

    public OrderDtoMapper(
        IOrderRepository orderRepository,
        IDeliveryTypeRepository deliveryTypeRepository,
        IPaymentMethodRepository paymentMethodRepository,
        IProductRepository productRepository
    )
    {
        this._orderRepository = orderRepository;
        this._deliveryTypeRepository = deliveryTypeRepository;
        this._paymentMethodRepository = paymentMethodRepository;
        this._productRepository = productRepository;
    }
    public async Task<Order> Map(OrderDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        int lastOrderNumber = await this._orderRepository.GetLastOrderNumber();
        int newOrderNumber = lastOrderNumber + 1;
        DeliveryType deliveryType = await this._deliveryTypeRepository.GetById(dto.DeliveryTypeId);
        PaymentMethod paymentMethod = await this._paymentMethodRepository.GetById(dto.PaymentMethodId);
        List<Product> products = await this._productRepository.GetProductsByIds(dto.ProductIds);

        return new Order(
            id,
            newOrderNumber,
            dto.Total,
            products,
            deliveryType,
            dto.ClientData,
            dto.Comment,
            dto.Status,
            paymentMethod
        );
    }
}