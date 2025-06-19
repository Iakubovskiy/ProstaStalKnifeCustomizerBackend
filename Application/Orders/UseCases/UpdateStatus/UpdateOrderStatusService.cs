using Domain.Order;
using Domain.Orders;
using Infrastructure.Orders;

namespace Application.Orders.UseCases.UpdateStatus;

public class UpdateOrderStatusService : IUpdateOrderStatusService
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderStatusService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> UpdateStatus(Guid id, string newStatus)
    {
        Order order = await this._orderRepository.GetById(id);
        order.ChangeStatus(newStatus);
        return await this._orderRepository.Update(id, order);
    }
}