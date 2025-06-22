using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Products.Filters.Colors;

public class ColorsFilterRepository : IColorsFilterRepository
{
    private readonly DBContext _context;

    public ColorsFilterRepository(DBContext context)
    {
        this._context = context;
    }
    
    public async Task<List<string>> GetAllColorFilters(string locale)
    {
        List<string> colors = new List<string>();

        List<string> bladeCoatingColors = await this._context.BladeCoatingColors
            .AsNoTracking()
            .Select(coating => coating.Color.GetTranslation(locale)).Distinct().ToListAsync();
        colors.AddRange(bladeCoatingColors);
        
        List<string> handleColors = await this._context.Handles
            .AsNoTracking()
            .Select(handle => handle.Color.GetTranslation(locale)).Distinct().ToListAsync();
        colors.AddRange(handleColors);
        
        List<string> sheathColors = await this._context.SheathColors
            .AsNoTracking()   
            .Select(sheathColor => sheathColor.Color.GetTranslation(locale)).Distinct().ToListAsync();
        colors.AddRange(sheathColors);
        
        return colors;
    }
}