using Application.Orders.Dto;
using Domain.Order;

namespace Application.Orders.UseCases.Create;

public interface ICreateOrderService
{
    public Task<Order> Create(OrderDto orderDto);
}