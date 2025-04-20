using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class EngravingPriceRepository : IRepository<EngravingPrice, Guid>
    {
        private readonly DBContext _context;
        public EngravingPriceRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<EngravingPrice>> GetAll()
        {
            return await _context.EngravingPrices.ToListAsync();
        }

        public async Task<EngravingPrice> GetById(Guid id)
        {
            return await _context.EngravingPrices.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("EngravingPrice not found");
        }

        public async Task<EngravingPrice> Create(EngravingPrice order)
        {
            _context.EngravingPrices.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<EngravingPrice> Update(Guid id, EngravingPrice newEngravingPrice)
        {
            EngravingPrice existingEngravingPrice = await _context.EngravingPrices.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("EngravingPrice not found");
            existingEngravingPrice.Price = newEngravingPrice.Price;
            await _context.SaveChangesAsync();
            return existingEngravingPrice;
        }

        public async Task<bool> Delete(Guid id)
        {
            EngravingPrice engravingPrice = await _context.EngravingPrices.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("EngravingPrice not found");
            _context.EngravingPrices.Remove(engravingPrice);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
