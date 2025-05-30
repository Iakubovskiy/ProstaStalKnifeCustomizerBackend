using Domain.Order;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Orders;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(DBContext context) : base(context)
    {
    }

    public async Task<int> GetLastOrderNumber()
    {
        Order? order = await Context.Orders.AsNoTracking().OrderBy(o => o.Id).FirstOrDefaultAsync();
        return order?.Number ?? 0;
    }
}