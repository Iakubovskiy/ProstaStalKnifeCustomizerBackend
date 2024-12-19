using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class DeliveryTypeRepository : Repository<DeliveryType, int>
    {
        private readonly DBContext _context;
        public DeliveryTypeRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<DeliveryType>> GetAll()
        {
            return await _context.DeliveryTypes.ToListAsync();
        }

        public async Task<DeliveryType> GetById(int id)
        {
            return await _context.DeliveryTypes.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<DeliveryType> Create(DeliveryType DeliveryType)
        {
            _context.DeliveryTypes.Add(DeliveryType);
            await _context.SaveChangesAsync();
            return DeliveryType;
        }

        public async Task<DeliveryType> Update(int id, DeliveryType newDeliveryType)
        {
            var existingDeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(a => a.Id == id);
            existingDeliveryType.Name = newDeliveryType.Name;
            existingDeliveryType.Price = newDeliveryType.Price;
            existingDeliveryType.Comment = newDeliveryType.Comment;
            await _context.SaveChangesAsync();
            return existingDeliveryType;
        }

        public async Task<bool> Delete(int id)
        {
            var DeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(a => a.Id == id);
            _context.DeliveryTypes.Remove(DeliveryType);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
