using Domain.Component.Handles;
using Microsoft.AspNetCore.Http;

namespace Application.Components.TexturedComponents.Data.Dto.HandleColors;

public class HandleColorDto : ITexturedComponentDto<Handle>
{
    public HandleColorDto(
        Dictionary<string, string> color,
        string? colorCode,
        bool isActive,
        Dictionary<string, string> material,
        Guid textureId,
        double price,
        Guid? colorMapId,
        Guid? handleModelId
    )
    {
        this.Color = color;
        this.ColorCode = colorCode;
        this.IsActive = isActive;
        this.Material = material;
        this.TextureId = textureId;
        this.ColorMapId = colorMapId;
        this.Price = price;
        this.HandleModelId = handleModelId;
    }
    
    public Dictionary<string, string> Color { get; private set; }
    public string? ColorCode { get; private set; }
    public bool IsActive { get; private set; }
    public Dictionary<string, string> Material { get; private set; }
    public Guid? TextureId { get; set; }
    public Guid? ColorMapId { get; private set; }
    public Guid? HandleModelId { get; private set; }
    public double Price { get; private set; }
}