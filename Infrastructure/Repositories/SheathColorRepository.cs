using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using DbContext = Infrastructure.Data.DbContext;

namespace Infrastructure.Repositories
{
    public class SheathColorRepository : IRepository<SheathColor, Guid>
    {
        private readonly DbContext _context;
        public SheathColorRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<List<SheathColor>> GetAll()
        {
            return await _context.SheathColors.ToListAsync();
        }

        public async Task<SheathColor> GetById(Guid id)
        {
            return await _context.SheathColors.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"Sheath Color with Id {id} not found");
        }

        public async Task<SheathColor> Create(SheathColor order)
        {
            _context.SheathColors.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<SheathColor> Update(Guid id, SheathColor newSheathColor)
        {
            SheathColor existingSheathColor = await _context.SheathColors.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"Sheath Color with Id {id} not found");
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

        public async Task<bool> Delete(Guid id)
        {
            SheathColor sheathColor = await _context.SheathColors.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"Sheath Color with Id {id} not found");
            _context.SheathColors.Remove(sheathColor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
