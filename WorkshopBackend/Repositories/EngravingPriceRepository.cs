using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class EngravingPriceRepository : Repository<EngravingPrice, int>
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

        public async Task<EngravingPrice> GetById(int id)
        {
            return await _context.EngravingPrices.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<EngravingPrice> Create(EngravingPrice EngravingPrice)
        {
            _context.EngravingPrices.Add(EngravingPrice);
            await _context.SaveChangesAsync();
            return EngravingPrice;
        }

        public async Task<EngravingPrice> Update(int id, EngravingPrice newEngravingPrice)
        {
            var existingEngravingPrice = await _context.EngravingPrices.FirstOrDefaultAsync(a => a.Id == id);
            existingEngravingPrice.Price = newEngravingPrice.Price;
            await _context.SaveChangesAsync();
            return existingEngravingPrice;
        }

        public async Task<bool> Delete(int id)
        {
            var EngravingPrice = await _context.EngravingPrices.FirstOrDefaultAsync(a => a.Id == id);
            _context.EngravingPrices.Remove(EngravingPrice);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
