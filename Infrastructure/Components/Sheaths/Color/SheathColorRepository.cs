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

    public async override Task<List<SheathColor>> GetAll()
    {
        return await this.Set
            .Include(color => color.Texture)
            .Include(color => color.Prices)
            .ToListAsync();
    }
    
    public async Task<List<SheathColor>> GetAllActive()
    {
        return await this.Set
            .Where(component => component.IsActive)
            .Include(color => color.Texture)
            .Include(color => color.Prices)
            .ToListAsync();
    }
}