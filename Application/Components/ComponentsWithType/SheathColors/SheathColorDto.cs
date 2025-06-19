using Domain.Component.Sheaths.Color;

namespace Application.Components.ComponentsWithType.SheathColors;

public class SheathColorDto
{
    public Guid? Id { get; set; }
    public Dictionary<string, string> Colors { get; set; }
    public Dictionary<string, string> Materials { get; set; }
    public string? ColorCode { get; set; }
    public string EngravingColorCode { get; set; }
    public List<SheathColorPriceByTypeDto> Prices { get; set; }
    public bool IsActive { get; set; }
    public Guid? TextureId { get; set; }
    public Guid? ColorMapId { get; set; }
}