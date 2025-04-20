using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class OrderStatusesRepository : IRepository<OrderStatuses, Guid>
    {
        private readonly DBContext _context;
        public OrderStatusesRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<OrderStatuses>> GetAll()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        public async Task<OrderStatuses> GetById(Guid id)
        {
            return await _context.OrderStatuses.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"OrderStatuses with id {id} not found");
        }

        public async Task<OrderStatuses> Create(OrderStatuses order)
        {
            _context.OrderStatuses.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<OrderStatuses> Update(Guid id, OrderStatuses newOrderStatuses)
        {
            OrderStatuses existingOrderStatuses = await _context.OrderStatuses.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"OrderStatuses with id {id} not found");
            existingOrderStatuses.Status = newOrderStatuses.Status;
            await _context.SaveChangesAsync();
            return existingOrderStatuses;
        }

        public async Task<bool> Delete(Guid id)
        {
            OrderStatuses orderStatuses = await _context.OrderStatuses.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"OrderStatuses with id {id} not found");
            _context.OrderStatuses.Remove(orderStatuses);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
