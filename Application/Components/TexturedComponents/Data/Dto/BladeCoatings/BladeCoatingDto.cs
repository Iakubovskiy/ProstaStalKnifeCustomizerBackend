using Domain.Component.BladeCoatingColors;

namespace Application.Components.TexturedComponents.Data.Dto.BladeCoatings;

public class BladeCoatingDto : ITexturedComponentDto<BladeCoatingColor>
{
    public BladeCoatingDto(
        Dictionary<string, string> type,
        Dictionary<string, string> color,
        string? colorCode,
        string engravingColorCode,
        Guid? colorMapId,
        double price,
        Guid? textureId
    )
    {
        this.Type = type;
        this.Color = color;
        this.ColorCode = colorCode;
        this.EngravingColorCode = engravingColorCode;
        this.ColorMapId = colorMapId;
        this.TextureId = textureId;
        this.Price = price;
    }
    
    public Dictionary<string, string> Type { get; private set; }
    public Dictionary<string, string> Color { get; private set; }
    public string? ColorCode { get; private set; }
    public string EngravingColorCode { get; private set; }
    public Guid? ColorMapId { get; private set; }
    public double Price { get; private set; }
    public Guid? TextureId { get; set; }
}