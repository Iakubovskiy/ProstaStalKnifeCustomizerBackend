using Infrastructure.Data;

namespace Infrastructure.Components.Products.Filters.Characteristics;

public class GetBladeShapeCharacteristicsFilterRepository : IGetBladeShapeCharacteristicsFilterRepository
{
    private readonly DBContext _context;

    public GetBladeShapeCharacteristicsFilterRepository(DBContext context)
    {
        this._context = context;
    }
    
    public async Task<(double, double)> GetBladeLengthFilters()
    {
        double min = this._context.BladeShapes.Min(shape => shape.BladeCharacteristics.BladeLength);
        double max = this._context.BladeShapes.Max(shape => shape.BladeCharacteristics.BladeLength);
        
        return (min, max);
    }

    public async Task<(double, double)> GetTotalLengthFilters()
    {
        double min = this._context.BladeShapes.Min(shape => shape.BladeCharacteristics.TotalLength);
        double max = this._context.BladeShapes.Max(shape => shape.BladeCharacteristics.TotalLength);
        
        return (min, max);
    }

    public async Task<(double, double)> GetBladeWidthFilters()
    {
        double min = this._context.BladeShapes.Min(shape => shape.BladeCharacteristics.BladeWidth);
        double max = this._context.BladeShapes.Max(shape => shape.BladeCharacteristics.BladeWidth);
        
        return (min, max);
    }

    public async Task<(double, double)> GetBladeWeightFilters()
    {
        double min = this._context.BladeShapes.Min(shape => shape.BladeCharacteristics.BladeWeight);
        double max = this._context.BladeShapes.Max(shape => shape.BladeCharacteristics.BladeWeight);
        
        return (min, max);
    }
}