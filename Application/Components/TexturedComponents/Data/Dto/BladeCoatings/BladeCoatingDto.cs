using Domain.Component.BladeCoatingColors;
using Microsoft.AspNetCore.Http;

namespace Application.Components.TexturedComponents.Data.Dto.BladeCoatings;

public class BladeCoatingDto : ITexturedComponentDto<BladeCoatingColor>
{
    public BladeCoatingDto(
        string type,
        string description,
        string color,
        string? colorCode,
        string engravingColorCode,
        IFormFile? colorMap,
        double price,
        Guid? textureId
    )
    {
        this.Type = type;
        this.Description = description;
        this.Color = color;
        this.ColorCode = colorCode;
        this.EngravingColorCode = engravingColorCode;
        this.ColorMap = colorMap;
        this.TextureId = textureId;
        this.Price = price;
    }
    
    public string Type { get; private set; }
    public string Description { get; private set; }
    public string Color { get; private set; }
    public string? ColorCode { get; private set; }
    public string EngravingColorCode { get; private set; }
    public IFormFile? ColorMap { get; private set; }
    public double Price { get; private set; }
    public Guid? TextureId { get; set; }
}