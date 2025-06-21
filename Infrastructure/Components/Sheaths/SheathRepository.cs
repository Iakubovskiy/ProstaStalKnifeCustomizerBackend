using System.Data.Entity.Core;
using Domain.Component.Sheaths;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Sheaths;

public class SheathRepository: ComponentRepository<Sheath>
{
    public SheathRepository(DBContext context)
    :base(context)
    {
        
    }
    
    public override async Task<List<Sheath>> GetAll()
    {
        return await this.Set
            .Include(sheath => sheath.Type)
            .ToListAsync();
    }
    
    public override async Task<List<Sheath>> GetAllActive()
    {
        return await this.Set
                    .Where(sheath => sheath.IsActive == true)
                    .Include(sheath => sheath.Type)
                    .ToListAsync();
    }

    public override async Task<Sheath> GetById(Guid id)
    {
        return await this.Set
                .Include(sheath => sheath.Type)
                .Include(sheath => sheath.Model)
                .FirstOrDefaultAsync(sheath => sheath.Id == id)
                ?? throw new ObjectNotFoundException("Entity not found");
    }
}