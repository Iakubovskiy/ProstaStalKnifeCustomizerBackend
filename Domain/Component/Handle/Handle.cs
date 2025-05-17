using Domain.Component.Translation;

namespace Domain.Component.Handle;

public class Handle : IComponent, IEntity
{
    public Guid Id { get; }
    public Translations Name { get; private set; }
    public string? ColorCode { get; private set; }
    public bool IsActive { get; set; }
    public Translations Material { get; private set; }
    public Texture.Texture? Texture { get; private set; }
    public string? ColorMapUrl { get; private set; }

    private Handle()
    {
        
    }

    public Handle(Guid id, Translations name, string? colorCode, bool isActive, Translations material,
        Texture.Texture? texture, string? colorMapUrl)
    {
        Id = id;
        Name = name;
        ColorCode = colorCode;
        IsActive = isActive;
        Material = material;
        Texture = texture;
        ColorMapUrl = colorMapUrl;
    }

    public double GetPrice()
    {
        throw new NotImplementedException();
    }
}