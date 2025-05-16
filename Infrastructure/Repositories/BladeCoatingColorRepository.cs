using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class BladeCoatingColorRepository : IRepository<BladeCoatingColor, Guid>
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

        public async Task<BladeCoatingColor> GetById(Guid id)
        {
            return await _context.BladeCoatingColors.FirstOrDefaultAsync(a => a.Id == id) 
                   ?? throw new Exception("Blade Coating Color not found");
        }

        public async Task<BladeCoatingColor> Create(BladeCoatingColor bladeCoatingColor)
        {
            _context.BladeCoatingColors.Add(bladeCoatingColor);
            await _context.SaveChangesAsync();
            return bladeCoatingColor;
        }

        public async Task<BladeCoatingColor> Update(Guid id, BladeCoatingColor newBladeCoatingColor)
        {
            BladeCoatingColor existingBladeCoatingColor = await _context.BladeCoatingColors.FirstOrDefaultAsync(a => a.Id == id) 
                                                          ?? throw new Exception("Blade Coating Color not found");
            existingBladeCoatingColor.Color = newBladeCoatingColor.Color;
            existingBladeCoatingColor.ColorCode = newBladeCoatingColor.ColorCode;
            existingBladeCoatingColor.EngravingColorCode = newBladeCoatingColor.EngravingColorCode;
            existingBladeCoatingColor.Type = newBladeCoatingColor.Type;
            if (newBladeCoatingColor.ColorMapUrl != null)
            {
                existingBladeCoatingColor.ColorMapUrl = newBladeCoatingColor.ColorMapUrl;
            }
            if (newBladeCoatingColor.NormalMapUrl != null)
            {
                existingBladeCoatingColor.NormalMapUrl = newBladeCoatingColor.NormalMapUrl;
            }
            if (newBladeCoatingColor.RoughnessMapUrl != null)
            {
                existingBladeCoatingColor.RoughnessMapUrl = newBladeCoatingColor.RoughnessMapUrl;
            }
            await _context.SaveChangesAsync();
            return existingBladeCoatingColor;
        }

        public async Task<bool> Delete(Guid id)
        {
            BladeCoatingColor bladeCoatingColor = await _context.BladeCoatingColors.FirstOrDefaultAsync(a => a.Id == id) 
                                                  ?? throw new Exception("Blade Coating Color not found");
            _context.BladeCoatingColors.Remove(bladeCoatingColor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
