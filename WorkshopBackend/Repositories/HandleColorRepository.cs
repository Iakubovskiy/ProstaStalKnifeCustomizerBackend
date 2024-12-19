﻿using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class HandleColorRepository : Repository<HandleColor, int>
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

        public async Task<HandleColor> GetById(int id)
        {
            return await _context.HandleColors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<HandleColor> Create(HandleColor handleColor)
        {
            _context.HandleColors.Add(handleColor);
            await _context.SaveChangesAsync();
            return handleColor;
        }

        public async Task<HandleColor> Update(int id, HandleColor newHandleColor)
        {
            var existingHandleColor = await _context.HandleColors.FirstOrDefaultAsync(a => a.Id == id);
            existingHandleColor.ColorName = newHandleColor.ColorName;
            existingHandleColor.ColorCode = newHandleColor.ColorCode;
            existingHandleColor.Material = newHandleColor.Material;
            if(newHandleColor.MaterialUrl != null)
            {
                existingHandleColor.MaterialUrl = newHandleColor.MaterialUrl;
            }

            await _context.SaveChangesAsync();
            return existingHandleColor;
        }

        public async Task<bool> Delete(int id)
        {
            var handleColor = await _context.HandleColors.FirstOrDefaultAsync(a => a.Id == id);
            _context.HandleColors.Remove(handleColor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}