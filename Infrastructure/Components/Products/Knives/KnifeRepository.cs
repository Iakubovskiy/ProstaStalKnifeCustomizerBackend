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
            .ThenInclude(blade => blade.Type)
            .Include(product => product.Blade)
            .ThenInclude(product => product.BladeShapeModel)
            .Include(product => product.Blade)
            .ThenInclude(product => product.Sheath)
            .ThenInclude(sheath => sheath.Model)
            .Include(product => product.Color)
            .Include(product => product.Handle)
            .Include(product => product.Sheath)
            .Include(product => product.SheathColor)
            .ThenInclude(sc => sc.Prices)
            .ThenInclude(prices => prices.Type)
            .Include(product => product.Engravings)
            .Include(product => product.Attachments)
            .Include(product => product.Image)
            .ToListAsync();
    }
    
    public override async Task<List<Knife>> GetAllActive()
    {
        return await this.Set
            .Where(product => product.IsActive)
            .Include(product => product.Tags)
            .Include(product => product.Blade)
            .ThenInclude(product => product.BladeShapeModel)
            .Include(product => product.Blade)
            .ThenInclude(product => product.Sheath)
            .ThenInclude(sheath => sheath.Model)
            .Include(product => product.Color)
            .Include(product => product.Handle)
            .Include(product => product.Sheath)
            .Include(product => product.SheathColor)
            .ThenInclude(sc => sc.Prices)
            .ThenInclude(prices => prices.Type)
            .Include(product => product.Engravings)
            .Include(product => product.Attachments)
            .Include(product => product.Image)
            .Include(product => product.Reviews)
            .ThenInclude(review => review.User)
            .ToListAsync();
    }

    public override async Task<Knife> GetById(Guid id)
    {
        return await this.Set
                   .Include(product => product.Tags)
                   .Include(product => product.Blade)
                   .ThenInclude(blade => blade.Type)
                   .Include(product => product.Blade)
                   .ThenInclude(product => product.BladeShapeModel)
                   .Include(product => product.Blade)
                   .ThenInclude(product => product.Sheath)
                   .ThenInclude(sheath => sheath.Model)
                   .Include(product => product.Color)
                   .Include(product => product.Handle)
                   .Include(product => product.Sheath)
                   .Include(product => product.SheathColor)
                   .ThenInclude(sc => sc.Prices)
                   .ThenInclude(prices => prices.Type)
                   .Include(product => product.Engravings)
                   .ThenInclude(engravings => engravings.Picture)
                   .Include(product => product.Image)
                   .Include(product => product.Attachments)
                   .Include(product => product.Reviews)
                   .ThenInclude(review => review.User)
                   .FirstOrDefaultAsync(product => product.Id == id)
               ?? throw new ObjectNotFoundException($"Entity not found {nameof(Knife)}");
    }
}