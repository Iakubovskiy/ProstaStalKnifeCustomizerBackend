namespace Domain.Component.SheathColor;

public class SheathColor : IEntity
{
    public Guid Id { get; }
    public string Color { get; private set; }
    public BladeShapeType.BladeShapeType Type { get; private set; }
    public bool IsActive { get; private set; }
    public string Material { get; private set; }
    public string EngravingColorcode { get; private set; }
    public Texture.Texture? Texture { get; private set; }
    public string? ColorMapUrl { get; private set; }

    private SheathColor()
    {
        
    }

    public SheathColor(Guid id, string color, BladeShapeType.BladeShapeType type, bool isActive,
        string material, string engravingColorcode, Texture.Texture? texture, string? colorMapUrl)
    {
        Id = id;
        Color = color;
        Type = type;
        IsActive = isActive;
        Material = material;
        EngravingColorcode = engravingColorcode;
        Texture = texture;
        ColorMapUrl = colorMapUrl;
    }
}