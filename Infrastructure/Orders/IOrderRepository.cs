using Domain.Orders;

namespace Infrastructure.Orders;

public interface IOrderRepository : IRepository<Order>
{
    public Task<int> GetLastOrderNumber();
}