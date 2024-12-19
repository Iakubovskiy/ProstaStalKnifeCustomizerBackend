using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class OrderStatusesRepository : Repository<OrderStatuses, int>
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

        public async Task<OrderStatuses> GetById(int id)
        {
            return await _context.OrderStatuses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<OrderStatuses> Create(OrderStatuses OrderStatuses)
        {
            _context.OrderStatuses.Add(OrderStatuses);
            await _context.SaveChangesAsync();
            return OrderStatuses;
        }

        public async Task<OrderStatuses> Update(int id, OrderStatuses newOrderStatuses)
        {
            var existingOrderStatuses = await _context.OrderStatuses.FirstOrDefaultAsync(a => a.Id == id);
            existingOrderStatuses.Status = newOrderStatuses.Status;
            await _context.SaveChangesAsync();
            return existingOrderStatuses;
        }

        public async Task<bool> Delete(int id)
        {
            var OrderStatuses = await _context.OrderStatuses.FirstOrDefaultAsync(a => a.Id == id);
            _context.OrderStatuses.Remove(OrderStatuses);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
