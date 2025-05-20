using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using DbContext = Infrastructure.Data.DbContext;

namespace Infrastructure.Repositories
{
    public class BladeShapeRepository : IRepository<BladeShape, Guid>
    {
        private readonly DbContext _context;
        public BladeShapeRepository(DbContext context)
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
            existingBladeShape.TotalLength = newBladeShape.TotalLength;
            existingBladeShape.BladeLength = newBladeShape.BladeLength;
            existingBladeShape.BladeWidth = newBladeShape.BladeWidth;
            existingBladeShape.BladeWeight = newBladeShape.BladeWeight;
            existingBladeShape.SharpeningAngle = newBladeShape.SharpeningAngle;
            existingBladeShape.RockwellHardnessUnits = newBladeShape.RockwellHardnessUnits;
            existingBladeShape.IsActive = newBladeShape.IsActive;

            if (!string.IsNullOrEmpty(newBladeShape.BladeShapeModelUrl))
                existingBladeShape.BladeShapeModelUrl = newBladeShape.BladeShapeModelUrl;

            if (!string.IsNullOrEmpty(newBladeShape.SheathModelUrl))
                existingBladeShape.SheathModelUrl = newBladeShape.SheathModelUrl;
            
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
