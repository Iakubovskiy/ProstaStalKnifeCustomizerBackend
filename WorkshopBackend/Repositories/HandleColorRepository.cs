using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class HandleColorRepository : IRepository<HandleColor, Guid>
    {
        private readonly DBContext _context;
        public HandleColorRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<HandleColor>> GetAll()
        {
            return await _context.HandleColors.ToListAsync();
        }

        public async Task<HandleColor> GetById(Guid id)
        {
            return await _context.HandleColors.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("HandleColor not found");
        }

        public async Task<HandleColor> Create(HandleColor order)
        {
            _context.HandleColors.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<HandleColor> Update(Guid id, HandleColor newHandleColor)
        {
            HandleColor existingHandleColor = await _context.HandleColors.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("HandleColor not found");
            existingHandleColor.ColorName = newHandleColor.ColorName;
            existingHandleColor.ColorCode = newHandleColor.ColorCode;
            existingHandleColor.Material = newHandleColor.Material;
            if (newHandleColor.ColorMapUrl != null)
            {
                existingHandleColor.ColorMapUrl = newHandleColor.ColorMapUrl;
            }
            if (newHandleColor.NormalMapUrl != null)
            {
                existingHandleColor.NormalMapUrl = newHandleColor.NormalMapUrl;
            }
            if (newHandleColor.RoughnessMapUrl != null)
            {
                existingHandleColor.RoughnessMapUrl = newHandleColor.RoughnessMapUrl;
            }

            await _context.SaveChangesAsync();
            return existingHandleColor;
        }

        public async Task<bool> Delete(Guid id)
        {
            HandleColor handleColor = await _context.HandleColors.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("HandleColor not found");
            _context.HandleColors.Remove(handleColor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
