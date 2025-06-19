using System.Data.Entity.Core;
using Domain.Component.Product.CompletedSheath;
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
            .Include(product => product.Image)
            .Include(product => product.Reviews)
            .Include(product => product.Sheath)
            .Include(product => product.SheathColor)
            .ThenInclude(color => color.ColorMap)
            .Include(product => product.SheathColor)
            .ThenInclude(color => color.Prices)
            .Include(product => product.Engravings)
            .ThenInclude(e => e.Tags)
            .Include(product => product.Attachments)
            .ThenInclude(a => a.Type)
            .ToListAsync();
    }
    
    public override async Task<List<CompletedSheath>> GetAllActive()
    {
        return await this.Set
            .Where(product => product.IsActive)
            .Include(product => product.Tags)
            .Include(product => product.Image)
            .Include(product => product.Reviews)
            .Include(product => product.Sheath)
            .Include(product => product.SheathColor)
            .ThenInclude(color => color.ColorMap)
            .Include(product => product.SheathColor)
            .ThenInclude(color => color.Prices)
            .Include(product => product.Engravings)
            .ThenInclude(e => e.Tags)
            .Include(product => product.Attachments)
            .ThenInclude(a => a.Type)
            .ToListAsync();
    }

    public override async Task<CompletedSheath> GetById(Guid id)
    {
        return await this.Set
                   .Include(product => product.Tags)
                   .Include(product => product.Image)
                   .Include(product => product.Reviews)
                   .Include(product => product.Sheath)
                   .Include(product => product.SheathColor)
                   .ThenInclude(color => color.ColorMap)
                   .Include(product => product.SheathColor)
                   .ThenInclude(color => color.Prices)
                   .Include(product => product.Engravings)
                   .ThenInclude(e => e.Tags)
                   .Include(product => product.Engravings)
                   .ThenInclude(e => e.Picture)
                   .Include(product => product.Attachments)
                   .ThenInclude(a => a.Type)
                   .FirstOrDefaultAsync(product => product.Id == id)
               ?? throw new ObjectNotFoundException("Entity not found");
    }
}