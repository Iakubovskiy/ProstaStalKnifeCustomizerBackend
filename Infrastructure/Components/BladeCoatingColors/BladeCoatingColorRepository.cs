using System.Data.Entity.Core;
using Domain.Component.BladeCoatingColors;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.BladeCoatingColors;

public class BladeCoatingColorRepository : ComponentRepository<BladeCoatingColor>
{
    public BladeCoatingColorRepository(DBContext context)
        : base(context)
    {
        
    }
    
    public override async Task<List<BladeCoatingColor>> GetAll()
    {
        return await this.Set
                   .Include(coatingColor => coatingColor.Texture)
                   .Include(coatingColor => coatingColor.ColorMap)
                   .ToListAsync();
    }
    
    public override async Task<List<BladeCoatingColor>> GetAllActive()
    {
        return await this.Set
                   .Where(coatingColor => coatingColor.IsActive)
                   .Include(coatingColor => coatingColor.Texture)
                   .Include(coatingColor => coatingColor.ColorMap)
                   .ToListAsync();
    }
    
    public override async Task<BladeCoatingColor> GetById(Guid id)
    {
        return await this.Set
            .Include(coatingColor => coatingColor.Texture)
            .Include(coatingColor => coatingColor.ColorMap)
            .FirstOrDefaultAsync(coatingColor => coatingColor.Id == id) 
               ?? throw new ObjectNotFoundException("Entity not found");
    }
}