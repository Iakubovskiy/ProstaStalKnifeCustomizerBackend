using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class BladeCoatingColorRepository : Repository<BladeCoatingColor, int>
    {
        private readonly DBContext _context;
        public BladeCoatingColorRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<BladeCoatingColor>> GetAll()
        {
            return await _context.BladeCoatingColors.ToListAsync();
        }

        public async Task<BladeCoatingColor> GetById(int id)
        {
            return await _context.BladeCoatingColors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<BladeCoatingColor> Create(BladeCoatingColor BladeCoatingColor)
        {
            _context.BladeCoatingColors.Add(BladeCoatingColor);
            await _context.SaveChangesAsync();
            return BladeCoatingColor;
        }

        public async Task<BladeCoatingColor> Update(int id, BladeCoatingColor newBladeCoatingColor)
        {
            var existingBladeCoatingColor = await _context.BladeCoatingColors.FirstOrDefaultAsync(a => a.Id == id);
            existingBladeCoatingColor.Color = newBladeCoatingColor.Color;
            existingBladeCoatingColor.ColorCode = newBladeCoatingColor.ColorCode;
            existingBladeCoatingColor.EngravingColorCode = newBladeCoatingColor.EngravingColorCode;
            await _context.SaveChangesAsync();
            return existingBladeCoatingColor;
        }

        public async Task<bool> Delete(int id)
        {
            var BladeCoatingColor = await _context.BladeCoatingColors.FirstOrDefaultAsync(a => a.Id == id);
            _context.BladeCoatingColors.Remove(BladeCoatingColor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
