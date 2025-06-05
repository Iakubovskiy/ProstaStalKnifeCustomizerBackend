using Domain.Component.Handles;
using Microsoft.AspNetCore.Http;

namespace Application.Components.TexturedComponents.Data.Dto.HandleColors;

public class HandleColorDto : ITexturedComponentDto<Handle>
{
    public Guid? Id { get; set; }
    public Dictionary<string, string> Color { get; set; }
    public string? ColorCode { get; set; }
    public bool IsActive { get; set; }
    public Dictionary<string, string> Material { get; set; }
    public Guid? TextureId { get; set; }
    public Guid? ColorMapId { get; set; }
    public Guid? HandleModelId { get; set; }
    public double Price { get; set; }
}