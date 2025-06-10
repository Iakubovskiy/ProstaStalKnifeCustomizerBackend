using Domain.Component.Sheaths.Color;

namespace Application.Components.ComponentsWithType.UseCases.Get;

public class SheathColorMapper: ISheathColorMapper
{
    public async Task<SheathColorResponsePresenter> MapToPresenter(SheathColor sheathColor)
    {
        return new SheathColorResponsePresenter
        {
            Id = sheathColor.Id,
            Color = sheathColor.Color.TranslationDictionary,
            Material = sheathColor.Material.TranslationDictionary,
            ColorCode = sheathColor.ColorCode,
            EngravingColorCode = sheathColor.EngravingColorCode,
            IsActive = sheathColor.IsActive,
            TextureId = sheathColor.Texture?.Id,
            ColorMapId = sheathColor.ColorMap?.Id,
            Prices = sheathColor.Prices?.Select(p => new SheathColorPriceResponsePresenter
            {
                TypeId = p.TypeId,
                TypeName = p.Type?.Name ?? "Unknown",
                Price = p.Price
            }).ToList() ?? new List<SheathColorPriceResponsePresenter>()
        };
    }

    public async Task<List<SheathColorResponsePresenter>> MapToPresenter(IEnumerable<SheathColor> sheathColors)
    {
        var mappingTasks = sheathColors.Select(sc => this.MapToPresenter(sc));
        
        var resultsArray = await Task.WhenAll(mappingTasks);
        
        return resultsArray.ToList();
    }
}