using System.Data.Entity.Core;
using Domain.Component.Engravings;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Engravings;

public class EngravingRepository : ComponentRepository<Engraving>
{
    public EngravingRepository(DBContext context)
    : base(context)
    {
        
    }
    
    public override async Task<List<Engraving>> GetAll()
    {
        return await this.Set
            .Include(engraving => engraving.Tags)
            .Include(engraving => engraving.Picture)
            .ToListAsync();
    }

    public override async Task<List<Engraving>> GetAllActive()
    {
        return await this.Set.Where(component => component.IsActive)
            .Include(engraving => engraving.Tags)
            .Include(engraving => engraving.Picture)
            .ToListAsync();
    }
    public override async Task<Engraving> GetById(Guid id)
    {
        return await this.Set
            .Include(engraving => engraving.Tags)
            .Include(engraving => engraving.Picture)
            .FirstOrDefaultAsync(engraving => engraving.Id == id) 
               ?? throw new ObjectNotFoundException();
    }
}