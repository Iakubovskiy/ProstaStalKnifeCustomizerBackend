using Domain.Component.BladeCoatingColors;

namespace Application.Components.TexturedComponents.Data.Dto.BladeCoatings;

public class BladeCoatingDto : ITexturedComponentDto<BladeCoatingColor>
{
    public Guid? Id { get; set; }
    public Dictionary<string, string> Type { get; set; }
    public Dictionary<string, string> Color { get; set; }
    public string? ColorCode { get; set; }
    public string EngravingColorCode { get; set; }
    public Guid? ColorMapId { get; set; }
    public double Price { get; set; }
    public Guid? TextureId { get; set; }
}