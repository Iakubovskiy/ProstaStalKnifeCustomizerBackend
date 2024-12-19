using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class OrderRepository: Repository<Order, int> 
    {
        private readonly DBContext _context;
        public OrderRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order> GetById(int id)
        {
            return _context.Orders
                .Include(o => o.DeliveryType)
                .Include(o => o.Status)
                .Include(o => o.Knives)
                .FirstOrDefault(o => o.Id == id);
        }
        public async Task<Order> Create(Order order)
        {
            foreach (var knife in order.Knives)
            {
                _context.Entry(knife).State = EntityState.Unchanged;
            }
            _context.Attach(order.DeliveryType);
            _context.Attach(order.Status);

            _context.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Update(int id, Order newOrder)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.Knives)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
                throw new KeyNotFoundException($"Order with id {id} not found.");

            existingOrder.Knives.Clear();
            foreach (var knife in newOrder.Knives)
            {
                _context.Attach(knife);
                existingOrder.Knives.Add(knife);
            }
            _context.Attach(newOrder.DeliveryType);
            _context.Attach(newOrder.Status);
            existingOrder.DeliveryType = newOrder.DeliveryType;
            existingOrder.Status = newOrder.Status;
            
            existingOrder.Number = newOrder.Number;
            existingOrder.Total = newOrder.Total;
            existingOrder.ClientFullName = newOrder.ClientFullName;
            existingOrder.ClientPhoneNumber = newOrder.ClientPhoneNumber;
            existingOrder.Email = newOrder.Email;
            existingOrder.City = newOrder.City;
            existingOrder.CountryForDelivery = newOrder.CountryForDelivery;
            existingOrder.Comment = newOrder.Comment;

            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<bool> Delete(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(k => k.Id == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
