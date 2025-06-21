using System.Data.Entity.Core;
using Domain.Component.BladeShapes;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.BladeShapes;

public class BladeShapeRepository : ComponentRepository<BladeShape>
{
    public BladeShapeRepository(DBContext context) 
        : base(context)
    {
        
    }
    public override async Task<List<BladeShape>> GetAll()
    {
        return await this.Set
            .Include(shape => shape.Sheath)
            .Include(shape => shape.Type)
            .Include(shape => shape.BladeShapeModel)
            .ToListAsync();
    }

    public override async Task<List<BladeShape>> GetAllActive()
    {
        return await this.Set.Where(shape => shape.IsActive == true)
            .Include(shape => shape.Sheath)
            .Include(shape => shape.Type)
            .Include(shape => shape.BladeShapeModel)
            .ToListAsync();
    }
    
    public override async Task<BladeShape> GetById(Guid id)
    {
        return await this.Set
                .Include(shape => shape.Sheath)
                .Include(shape => shape.Type)
                .Include(shape => shape.BladeShapeModel)
                .FirstOrDefaultAsync(shape =>  shape.Id == id) 
                ?? throw new ObjectNotFoundException($"Entity not found {nameof(BladeShape)}");
    }
}