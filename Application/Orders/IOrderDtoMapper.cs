using Application.Orders.Dto;
using Domain.Order;

namespace Application.Orders;

public interface IOrderDtoMapper
{
    public Task<Order> Map(OrderDto orderDto);
}