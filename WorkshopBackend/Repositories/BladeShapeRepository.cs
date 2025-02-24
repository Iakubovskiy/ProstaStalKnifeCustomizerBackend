﻿using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class BladeShapeRepository : Repository<BladeShape, Guid>
    {
        private readonly DBContext _context;
        public BladeShapeRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<BladeShape>> GetAll()
        {
            return await _context.BladeShapes.ToListAsync();
        }

        public async Task<BladeShape> GetById(Guid id)
        {
            return await _context.BladeShapes.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<BladeShape> Create(BladeShape BladeShape)
        {
            _context.BladeShapes.Add(BladeShape);
            await _context.SaveChangesAsync();
            return BladeShape;
        }

        public async Task<BladeShape> Update(Guid id, BladeShape newBladeShape)
        {
            var existingBladeShape = await _context.BladeShapes.FirstOrDefaultAsync(a => a.Id == id);
            existingBladeShape.Name = newBladeShape.Name ?? existingBladeShape.Name;
            existingBladeShape.Price = newBladeShape.Price;
            existingBladeShape.totalLength = newBladeShape.totalLength;
            existingBladeShape.bladeLength = newBladeShape.bladeLength;
            existingBladeShape.bladeWidth = newBladeShape.bladeWidth;
            existingBladeShape.bladeWeight = newBladeShape.bladeWeight;
            existingBladeShape.sharpeningAngle = newBladeShape.sharpeningAngle;
            existingBladeShape.rockwellHardnessUnits = newBladeShape.rockwellHardnessUnits;
            existingBladeShape.IsActive = newBladeShape.IsActive;

            if (!string.IsNullOrEmpty(newBladeShape.bladeShapeModelUrl))
                existingBladeShape.bladeShapeModelUrl = newBladeShape.bladeShapeModelUrl;

            if (!string.IsNullOrEmpty(newBladeShape.sheathModelUrl))
                existingBladeShape.sheathModelUrl = newBladeShape.sheathModelUrl;
            
            await _context.SaveChangesAsync();
            return existingBladeShape;
        }

        public async Task<bool> Delete(Guid id)
        {
            var BladeShape = await _context.BladeShapes.FirstOrDefaultAsync(a => a.Id == id);
            _context.BladeShapes.Remove(BladeShape);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
