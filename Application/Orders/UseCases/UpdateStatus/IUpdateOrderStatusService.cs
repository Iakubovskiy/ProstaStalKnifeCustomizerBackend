using Domain.Orders;

namespace Application.Orders.UseCases.UpdateStatus;

public interface IUpdateOrderStatusService
{
    public Task<Order> UpdateStatus(Guid id, string newStatus);
}