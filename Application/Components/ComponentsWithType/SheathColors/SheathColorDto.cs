using Domain.Component.Sheaths.Color;

namespace Application.Components.ComponentsWithType.SheathColors;

public class SheathColorDto
{
    public SheathColorDto(
        Dictionary<string, string> colors,
        Dictionary<string, string> material,
        string? colorCode,
        string engravingColorCode,
        List<SheathColorPriceByType> prices,
        bool isActive,
        Guid? textureId,
        Guid? colorMapId, 
        Guid? id = null
    )
    {
        this.Color = colors;
        this.Material = material;
        this.ColorCode = colorCode;
        this.EngravingColorCode = engravingColorCode;
        this.Prices = prices;
        this.IsActive = isActive;
        this.TextureId = textureId;
        this.ColorMapId = colorMapId;
        this.Id = id;
    }
    
    public Guid? Id { get; private set; }
    public Dictionary<string, string> Color { get; private set; }
    public Dictionary<string, string> Material { get; private set; }
    public string? ColorCode { get; private set; }
    public string EngravingColorCode { get; private set; }
    public List<SheathColorPriceByType> Prices { get; private set; }
    public bool IsActive { get; private set; }
    public Guid? TextureId { get; private set; }
    public Guid? ColorMapId { get; private set; }
}