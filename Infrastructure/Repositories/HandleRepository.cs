using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using DbContext = Infrastructure.Data.DbContext;

namespace Infrastructure.Repositories
{
    public class HandleRepository : IRepository<Handle, Guid>
    {
        private readonly DbContext _context;
        public HandleRepository(DbContext context)
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
