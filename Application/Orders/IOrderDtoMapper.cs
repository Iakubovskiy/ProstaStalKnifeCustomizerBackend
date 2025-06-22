using Application.Orders.Dto;
using Domain.Orders;

namespace Application.Orders;

public interface IOrderDtoMapper
{
    public Task<Order> Map(OrderDto orderDto);
}