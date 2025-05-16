using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class OrderRepository: IRepository<Order, Guid> 
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
            return await  _context.Orders
                .Include(o => o.DeliveryType)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id)
                ?? throw new Exception($"Order with id {id} not found");
        }
        public async Task<Order> Create(Order order)
        {
            
            _context.Entry(order.DeliveryType).State = EntityState.Unchanged;
            
            foreach (var product in order.Products)
            {
                _context.Entry(product).State = EntityState.Unchanged;
            }

            _context.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Update(Guid id, Order newOrder)
        {
            Order existingOrder = await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id)
                ?? throw new Exception($"Order with id {id} not found");

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
            Order order = await _context.Orders.FirstOrDefaultAsync(k => k.Id == id)
                ?? throw new Exception($"Order with id {id} not found");
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
