using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Products.Filters.Styles;

public class FilterStylesRepository : IFilterStylesRepository
{
    private readonly DBContext _context;
    
    public FilterStylesRepository(DBContext context)
    {
        this._context = context;
    }
    
    public async Task<List<string>> GetAllStyleTags(string locale)
    {
        List<string> tags = 
            await this._context.EngravingTags
            .Select(tag => tag.Name.GetTranslation(locale))
            .Distinct()
            .ToListAsync();
        
        return tags;
    }
}