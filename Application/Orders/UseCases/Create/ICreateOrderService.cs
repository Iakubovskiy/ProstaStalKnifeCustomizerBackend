using Application.Orders.Dto;
using Domain.Order;
using Domain.Orders;

namespace Application.Orders.UseCases.Create;

public interface ICreateOrderService
{
    public Task<Order> Create(OrderDto orderDto, string locale, Guid? userId = null);
}