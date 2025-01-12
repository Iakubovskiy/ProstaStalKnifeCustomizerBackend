using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class OrderRepository: Repository<Order, Guid> 
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
        public async Task<Order> GetById(Guid id)
        {
            return _context.Orders
                .Include(o => o.DeliveryType)
                .Include(o => o.Products)
                .FirstOrDefault(o => o.Id == id);
        }
        public async Task<Order> Create(Order order)
        {
            if (order.DeliveryType != null)
            {
                _context.Entry(order.DeliveryType).State = EntityState.Unchanged;
            }

            if (order.Products != null)
            {
                foreach (var product in order.Products)
                {
                    _context.Entry(product).State = EntityState.Unchanged;
                }
            }

            _context.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Update(Guid id, Order newOrder)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
                throw new KeyNotFoundException($"Order with id {id} not found.");
            if (newOrder.Products.Count != 0)
            {
                existingOrder.Products.Clear();
                foreach (var knife in newOrder.Products)
                {
                    _context.Attach(knife);
                    existingOrder.Products.Add(knife);
                }
            }
            _context.Attach(newOrder.DeliveryType);
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

        public async Task<bool> Delete(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(k => k.Id == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
