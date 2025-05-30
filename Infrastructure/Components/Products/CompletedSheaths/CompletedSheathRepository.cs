using System.Data.Entity.Core;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Products.CompletedSheaths;

public class CompletedSheathRepository : ComponentRepository<CompletedSheath>
{
    public CompletedSheathRepository(DBContext context)
        : base(context)
    {
        
    }

    public override async Task<List<CompletedSheath>> GetAll()
    {
        return await this.Set
            .Include(product => product.Tags)
            .Include(product => product.Sheath)
            .Include(product => product.SheathColor)
            .Include(product => product.Engravings)
            .Include(product => product.Attachments)
            .ToListAsync();
    }
    
    public override async Task<List<CompletedSheath>> GetAllActive()
    {
        return await this.Set
            .Where(product => product.IsActive)
            .Include(product => product.Tags)
            .Include(product => product.Sheath)
            .Include(product => product.SheathColor)
            .Include(product => product.Engravings)
            .Include(product => product.Attachments)
            .ToListAsync();
    }

    public override async Task<CompletedSheath> GetById(Guid id)
    {
        return await this.Set
                   .Include(product => product.Tags)
                   .Include(product => product.Sheath)
                   .Include(product => product.SheathColor)
                   .Include(product => product.Engravings)
                   .Include(product => product.Attachments)
                   .FirstOrDefaultAsync(product => product.Id == id)
               ?? throw new ObjectNotFoundException("Entity not found");
    }
}