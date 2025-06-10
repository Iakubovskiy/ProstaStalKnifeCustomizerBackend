using Domain.Order;
using Domain.Order.Support;

namespace Application.Orders.UseCases.ChangeClientData;

public interface IChangeClientDataService
{
    public Task<Order> ChangeClientData(Guid id, ClientData order);
}