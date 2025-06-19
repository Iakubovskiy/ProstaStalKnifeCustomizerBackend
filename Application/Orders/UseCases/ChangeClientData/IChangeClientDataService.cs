using Domain.Orders;
using Domain.Orders.Support;
using Domain.Orders;

namespace Application.Orders.UseCases.ChangeClientData;

public interface IChangeClientDataService
{
    public Task<Order> ChangeClientData(Guid id, ClientData clientData);
}