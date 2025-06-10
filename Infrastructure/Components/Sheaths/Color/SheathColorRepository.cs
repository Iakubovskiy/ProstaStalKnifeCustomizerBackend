using System.Data.Entity.Core;
using Domain.Component.Sheaths.Color;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Sheaths.Color;

public class SheathColorRepository : BaseRepository<SheathColor>, ISheathColorRepository
{
    public SheathColorRepository(DBContext context) 
        : base(context)
    {
    }

    public override async Task<List<SheathColor>> GetAll()
    {
        return await this.Set
            .Include(color => color.Texture)
            .Include(color => color.Prices)
            .ThenInclude(price => price.Type)
            .Include(color => color.ColorMap)
            .ToListAsync();
    }
    
    public async Task<List<SheathColor>> GetAllActive()
    {
        return await this.Set
            .Where(component => component.IsActive)
            .Include(color => color.Prices)
            .ThenInclude(price => price.Type)
            .Include(color => color.ColorMap)
            .ToListAsync();
    }
    
    public override async Task<SheathColor> GetById(Guid id)
    {
        return await this.Set
            .Include(color => color.Texture)
            .Include(color => color.Prices)
            .ThenInclude(price => price.Type)
            .Include(color => color.ColorMap)
            .FirstOrDefaultAsync(color => color.Id == id)
            ?? throw new ObjectNotFoundException($"The sheath color with id {id} was not found.");
    }
}