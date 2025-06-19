using System.Data.Entity.Core;
using Domain.Component.Handles;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Handles;

public class HandleRepository : ComponentRepository<Handle>
{
    public HandleRepository(DBContext context) 
        : base(context)
    {
        
    }
    
    public override async Task<List<Handle>> GetAll()
    {
        return await this.Set
            .Include(coatingColor => coatingColor.Texture)
            .Include(coatingColor => coatingColor.ColorMap)
            .ToListAsync();
    }
    
    public override async Task<List<Handle>> GetAllActive()
    {
        return await this.Set
            .Where(coatingColor => coatingColor.IsActive)
            .Include(coatingColor => coatingColor.Texture)
            .Include(coatingColor => coatingColor.ColorMap)
            .ToListAsync();
    }
    
    public override async Task<Handle> GetById(Guid id)
    {
        return await this.Set
                   .Include(coatingColor => coatingColor.Texture)
                   .Include(coatingColor => coatingColor.ColorMap)
                   .FirstOrDefaultAsync(coatingColor => coatingColor.Id == id) 
               ?? throw new ObjectNotFoundException("Entity not found");
    }
}