using System.Data.Entity.Core;
using Domain.Component.Product;
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
                   .Include(knife => knife.Image)
                   
                   .Include(product => product.Blade)
                   .ThenInclude(blade => blade.Type)
                   .Include(product => product.Blade)
                   .ThenInclude(blade => blade.BladeShapeModel)
                   .Include(product => product.Blade)
                   .ThenInclude(blade => blade.Sheath)
                   .ThenInclude(sheath => sheath.Model)
                   
                   .Include(product => product.Color)
                   .ThenInclude(color => color.Texture)
                   .ThenInclude(t => t.NormalMap)
                   .Include(product => product.Color)
                   .ThenInclude(color => color.Texture)
                   .ThenInclude(t => t.RoughnessMap)
                   .Include(product => product.Color)
                   .ThenInclude(color => color.ColorMap)
                   
                   .Include(product => product.Handle)
                   .ThenInclude(handle => handle.Texture)
                   .ThenInclude(t => t.NormalMap)
                   .Include(product => product.Handle)
                   .ThenInclude(handle => handle.Texture)
                   .ThenInclude(t => t.RoughnessMap)
                   .Include(product => product.Handle)
                   .ThenInclude(handle => handle.HandleModel)
                   .Include(product => product.Handle)
                   .ThenInclude(handle => handle.ColorMap)
                   
                   .Include(product => product.Sheath)
                   
                   .Include(product => product.SheathColor)
                   .ThenInclude(color => color.Texture)
                   .ThenInclude(t => t.NormalMap)
                   .Include(product => product.SheathColor)
                   .ThenInclude(color => color.Texture)
                   .ThenInclude(t => t.RoughnessMap)
                   .Include(product => product.SheathColor)
                   .ThenInclude(sc => sc.Prices)
                   .Include(product => product.SheathColor)
                   .ThenInclude(sc => sc.ColorMap)
                   
                   .Include(product => product.Engravings)
                   .ThenInclude(engraving => engraving.Picture)
                   
                   .Include(product => product.Attachments)
                   .ThenInclude(a => a.Model)
                   
                   .FirstOrDefaultAsync(product => product.Id == id)
               ?? throw new ObjectNotFoundException("Entity not found");
    }
}