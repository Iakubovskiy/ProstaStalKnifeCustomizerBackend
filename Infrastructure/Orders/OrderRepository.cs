using System.Data.Entity.Core;
using Domain.Order;
using Domain.Orders;
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

    public override async Task<List<Order>> GetAll()
    {
        return await Set
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();
    }

    public override async Task<Order> GetById(Guid id)
    {
        return await Set
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id) ?? throw new ObjectNotFoundException("Order not found");
    }
}