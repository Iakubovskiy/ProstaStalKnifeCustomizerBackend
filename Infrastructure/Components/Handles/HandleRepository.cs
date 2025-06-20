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
            .Include(handle => handle.Texture)
            .Include(handle => handle.BladeShapeType)
            .Include(handle => handle.ColorMap)
            .ToListAsync();
    }
    
    public override async Task<List<Handle>> GetAllActive()
    {
        return await this.Set
            .Where(handle => handle.IsActive)
            .Include(handle => handle.Texture)
            .Include(handle => handle.ColorMap)
            .Include(handle => handle.BladeShapeType)
            .ToListAsync();
    }
    
    public override async Task<Handle> GetById(Guid id)
    {
        return await this.Set
                   .Include(handle => handle.Texture)
                   .Include(handle => handle.BladeShapeType)
                   .Include(handle => handle.ColorMap)
                   .FirstOrDefaultAsync(handle => handle.Id == id) 
               ?? throw new ObjectNotFoundException("Entity not found");
    }
}