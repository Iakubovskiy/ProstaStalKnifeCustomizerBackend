using Domain.Order;
using Domain.Order.Support;
using Infrastructure.Orders;

namespace Application.Orders.UseCases.ChangeClientData;

public class ChangeClientDataService : IChangeClientDataService
{
    private readonly IOrderRepository _orderRepository;

    public ChangeClientDataService(IOrderRepository orderRepository)
    {
        this._orderRepository = orderRepository;
    }

    public async Task<Order> ChangeClientData(Guid id, ClientData clientData)
    {
        Order existingOrder = await this._orderRepository.GetById(id);
        existingOrder.ChangeClientData(clientData);
        return await this._orderRepository.Update(id, existingOrder);
    }
}