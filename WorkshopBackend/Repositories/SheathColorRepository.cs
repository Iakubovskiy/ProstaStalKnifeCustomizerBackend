using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class SheathColorRepository : Repository<SheathColor, int>
    {
        private readonly DBContext _context;
        public SheathColorRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<SheathColor>> GetAll()
        {
            return await _context.SheathColors.ToListAsync();
        }

        public async Task<SheathColor> GetById(int id)
        {
            return await _context.SheathColors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<SheathColor> Create(SheathColor SheathColor)
        {
            _context.SheathColors.Add(SheathColor);
            await _context.SaveChangesAsync();
            return SheathColor;
        }

        public async Task<SheathColor> Update(int id, SheathColor newSheathColor)
        {
            var existingSheathColor = await _context.SheathColors.FirstOrDefaultAsync(a => a.Id == id);
            existingSheathColor.Color = newSheathColor.Color;
            existingSheathColor.ColorCode = newSheathColor.ColorCode;
            existingSheathColor.Material = newSheathColor.Material;
            existingSheathColor.Price = newSheathColor.Price;
            existingSheathColor.IsActive = newSheathColor.IsActive;
            if (newSheathColor.ColorMapUrl != null)
            {
                existingSheathColor.ColorMapUrl = newSheathColor.ColorMapUrl;
            }
            if (newSheathColor.NormalMapUrl != null)
            {
                existingSheathColor.NormalMapUrl = newSheathColor.NormalMapUrl;
            }
            if (newSheathColor.RoughnessMapUrl != null)
            {
                existingSheathColor.RoughnessMapUrl = newSheathColor.RoughnessMapUrl;
            }
            await _context.SaveChangesAsync();
            return existingSheathColor;
        }

        public async Task<bool> Delete(int id)
        {
            var SheathColor = await _context.SheathColors.FirstOrDefaultAsync(a => a.Id == id);
            _context.SheathColors.Remove(SheathColor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
