using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class BladeShapeRepository : IRepository<BladeShape, Guid>
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
            return await _context.BladeShapes.FirstOrDefaultAsync(a => a.Id == id) 
                   ?? throw new Exception("Blade shape not found");
        }

        public async Task<BladeShape> Create(BladeShape order)
        {
            _context.BladeShapes.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<BladeShape> Update(Guid id, BladeShape newBladeShape)
        {
            BladeShape existingBladeShape = await _context.BladeShapes.FirstOrDefaultAsync(a => a.Id == id) 
                                            ?? throw new Exception("Blade shape not found");
            existingBladeShape.Name = newBladeShape.Name;
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
            BladeShape bladeShape = await _context.BladeShapes.FirstOrDefaultAsync(a => a.Id == id) 
                                    ?? throw new Exception("Blade shape not found");
            _context.BladeShapes.Remove(bladeShape);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
