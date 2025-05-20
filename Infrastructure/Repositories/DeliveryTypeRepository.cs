using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using DbContext = Infrastructure.Data.DbContext;

namespace Infrastructure.Repositories
{
    public class DeliveryTypeRepository : IRepository<DeliveryType, Guid>
    {
        private readonly DbContext _context;
        public DeliveryTypeRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<List<DeliveryType>> GetAll()
        {
            return await _context.DeliveryTypes.ToListAsync();
        }

        public async Task<DeliveryType> GetById(Guid id)
        {
            return await _context.DeliveryTypes.FirstOrDefaultAsync(a => a.Id == id) 
                   ?? throw new Exception("DeliveryType not found");
        }

        public async Task<DeliveryType> Create(DeliveryType order)
        {
            _context.DeliveryTypes.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<DeliveryType> Update(Guid id, DeliveryType newDeliveryType)
        {
            DeliveryType existingDeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(a => a.Id == id) 
                                                ?? throw new Exception("DeliveryType not found");
            existingDeliveryType.Name = newDeliveryType.Name;
            existingDeliveryType.Price = newDeliveryType.Price;
            existingDeliveryType.Comment = newDeliveryType.Comment;
            existingDeliveryType.IsActive = newDeliveryType.IsActive;
            await _context.SaveChangesAsync();
            return existingDeliveryType;
        }

        public async Task<bool> Delete(Guid id)
        {
            DeliveryType deliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(a => a.Id == id) 
                                        ?? throw new Exception("DeliveryType not found");
            _context.DeliveryTypes.Remove(deliveryType);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
