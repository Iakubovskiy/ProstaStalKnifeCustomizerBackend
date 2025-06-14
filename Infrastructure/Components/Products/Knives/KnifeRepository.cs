using System.Data.Entity.Core;
using Domain.Component.Product.Knife;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Products.Knives;

public class KnifeRepository : ComponentRepository<Knife>
{
    public KnifeRepository(DBContext context)
    : base(context)
    {
        
    }

    public override async Task<List<Knife>> GetAll()
    {
        return await this.Set
            .Include(product => product.Tags)
            .Include(product => product.Blade)
            .Include(product => product.Color)
            .Include(product => product.Handle)
            .Include(product => product.Sheath)
            .Include(product => product.SheathColor)
            .Include(product => product.Engravings)
            .Include(product => product.Attachments)
            .ToListAsync();
    }
    
    public override async Task<List<Knife>> GetAllActive()
    {
        return await this.Set
            .Where(product => product.IsActive)
            .Include(product => product.Tags)
            .Include(product => product.Blade)
            .Include(product => product.Color)
            .Include(product => product.Handle)
            .Include(product => product.Sheath)
            .Include(product => product.SheathColor)
            .Include(product => product.Engravings)
            .Include(product => product.Attachments)
            .ToListAsync();
    }

    public override async Task<Knife> GetById(Guid id)
    {
        return await this.Set
                   .Include(product => product.Tags)
                   .Include(product => product.Blade)
                   .Include(product => product.Color)
                   .Include(product => product.Handle)
                   .Include(product => product.Sheath)
                   .Include(product => product.SheathColor)
                   .Include(product => product.Engravings)
                   .Include(product => product.Attachments)
                   .FirstOrDefaultAsync(product => product.Id == id)
               ?? throw new ObjectNotFoundException("Entity not found");
    }
}